using FoodDictionary.Services;

namespace FoodDictionary.Pages;

public partial class MyLists : ContentPage
{
    public MyLists()
    {
        InitializeComponent();

        // set theme color from app state
        var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
        if (appState != null)
            this.BackgroundColor = appState.BackgroundColor;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // re-apply theme color on reentry
        var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
        if (appState != null)
            this.BackgroundColor = appState.BackgroundColor;
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {

    }
}
