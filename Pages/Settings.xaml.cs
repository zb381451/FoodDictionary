using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using FoodDictionary.Services;

namespace FoodDictionary.Pages;

public partial class Settings : ContentPage
{
    public Settings()
    {
        InitializeComponent();

        // apply saved background color
        var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
        if (appState != null)
        {
            this.BackgroundColor = appState.BackgroundColor;
            BindingContext = appState;
        }
    }

    private void OnColorSelected(object sender, EventArgs e)
    {
        var appState = BindingContext as AppState;
        string? selected = ColorPicker.SelectedItem?.ToString();

        if (appState == null || string.IsNullOrEmpty(selected))
            return;

        appState.BackgroundColor = selected switch
        {
            "LightGray" => Colors.LightGray,
            "LightBlue" => Colors.LightBlue,
            "LightYellow" => Colors.LightYellow,
            "LightGreen" => Colors.LightGreen,
            _ => Colors.White
        };

        // update background after selection
        this.BackgroundColor = appState.BackgroundColor;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // re-apply saved background
        var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
        if (appState != null)
        {
            this.BackgroundColor = appState.BackgroundColor;
        }
    }
}
