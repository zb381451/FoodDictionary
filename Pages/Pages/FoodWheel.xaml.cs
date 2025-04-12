using Microsoft.Maui.Controls;
using FoodDictionary.Models;
using FoodDictionary.Services;

namespace FoodDictionary.Pages;
    public partial class FoodWheel : ContentPage
    {
    private readonly DatabaseService _databaseService;


    int count = 0;

    public FoodWheel()
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


    private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
