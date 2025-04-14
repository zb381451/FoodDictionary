using Microsoft.Maui.Controls;
using FoodDictionary.Models;
using FoodDictionary.Services; // required for AppState

namespace FoodDictionary.Pages;

public partial class AddFood : ContentPage
{
    private readonly DatabaseService _databaseService;

    public AddFood(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;

        // set bg from app state
        var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
        if (appState != null)
        {
            this.BackgroundColor = appState.BackgroundColor;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // restore bg
        var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
        if (appState != null)
        {
            this.BackgroundColor = appState.BackgroundColor;
        }
    }

    private async void OnAddFoodClicked(object sender, EventArgs e)
    {
        await _databaseService.InitAsync();

        string name = NameEntry.Text?.Trim();
        string serving = ServingSizeEntry.Text?.Trim();
        string priceText = PriceEntry.Text?.Trim();

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(serving))
        {
            await DisplayAlert("Missing Info", "Name and serving size are required.", "OK");
            return;
        }

        decimal? price = null;
        if (decimal.TryParse(priceText, out decimal parsedPrice))
        {
            price = parsedPrice;
        }

        var newFood = new Food
        {
            Name = name,
            Serving_Size = serving,
            Price = price
        };

        try
        {
            await DatabaseService.GetConnection().InsertAsync(newFood);

            StatusLabel.Text = "Food added successfully!";
            StatusLabel.IsVisible = true;

            NameEntry.Text = string.Empty;
            ServingSizeEntry.Text = string.Empty;
            PriceEntry.Text = string.Empty;
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Failed to add food: {ex.Message}";
            StatusLabel.TextColor = Microsoft.Maui.Graphics.Colors.Red;
            StatusLabel.IsVisible = true;
        }
    }
}
