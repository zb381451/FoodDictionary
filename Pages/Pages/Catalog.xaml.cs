using Microsoft.Maui.Controls;
using FoodDictionary.Models;
using FoodDictionary.Services;

namespace FoodDictionary.Pages;
public partial class Catalog : ContentPage
{
    
    public Catalog()
	{
		InitializeComponent();
        var appState = Application.Current.Handler.MauiContext.Services.GetService<AppState>();
        this.BackgroundColor = appState?.BackgroundColor ?? Colors.White;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        var appState = Application.Current.Handler.MauiContext.Services.GetService<AppState>();
        this.BackgroundColor = appState?.BackgroundColor ?? Colors.White;
    }
}