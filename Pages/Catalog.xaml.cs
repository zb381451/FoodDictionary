using FoodDictionary.Services;
using FoodDictionary.Models;
using System.Collections.ObjectModel; 

namespace FoodDictionary.Pages;

public partial class Catalog : ContentPage
{
    public ObservableCollection<Food> FoodItems { get; set; } = new();

    private readonly DatabaseService _databaseService = new();

    public Catalog()
    {
        InitializeComponent();
        BindingContext = this; 

        // set bg from app state
        var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
        if (appState != null)
        {
            this.BackgroundColor = appState.BackgroundColor;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // reapply bg
        var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
        if (appState != null)
        {
            this.BackgroundColor = appState.BackgroundColor;
        }

        // pull food items from DB and update list
        await _databaseService.InitAsync();
        var items = await DatabaseService.GetConnection().Table<Food>().ToListAsync();
        FoodItems.Clear();
        foreach (var food in items)
        {
            FoodItems.Add(food);
        }
    }

    // deletes item from DB and list
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var food = button?.CommandParameter as Food;
        if (food == null) return;

        bool confirm = await DisplayAlert("Delete", $"Delete {food.Name}?", "Yes", "No");
        if (confirm)
        {
            await DatabaseService.GetConnection().DeleteAsync(food);
            FoodItems.Remove(food);
        }
    }

    //lets user rename food name
    private async void OnEditClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var food = button?.CommandParameter as Food;
        if (food == null) return;

        string newName = await DisplayPromptAsync("Edit Name", "New name:", initialValue: food.Name);
        if (!string.IsNullOrWhiteSpace(newName))
        {
            food.Name = newName;
            await DatabaseService.GetConnection().UpdateAsync(food);
            OnAppearing(); // refresh
        }
    }
    // runs every time the user types into the search box
    private async void SearchTerm_TextChanged(object sender, TextChangedEventArgs e)
    {
        // make sure the database is ready
        await _databaseService.InitAsync();

        // grab all food items from the database
        var allItems = await DatabaseService.GetConnection().Table<Food>().ToListAsync();

        // get what the user typed (in lowercase for easy matching)
        string query = e.NewTextValue?.ToLower() ?? "";

        // filter the list by name
        var filteredItems = allItems
            .Where(food => food.Name?.ToLower().Contains(query) ?? false)
            .ToList();

        // show only the filtered items
        FoodItems.Clear();
        foreach (var item in filteredItems)
        {
            FoodItems.Add(item);
        }
    }
}
