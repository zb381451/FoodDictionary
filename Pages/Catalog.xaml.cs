using FoodDictionary.Services;

namespace FoodDictionary.Pages;

public partial class Catalog : ContentPage
{
    public Catalog()
    {
        InitializeComponent();

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
}
