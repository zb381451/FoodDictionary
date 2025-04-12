using Microsoft.Maui.Controls;
using FoodDictionary.Models;
using FoodDictionary.Services;

namespace FoodDictionary.Pages;

public partial class MyLists : ContentPage
{
    private readonly DatabaseService _databaseService;

    public MyLists()
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