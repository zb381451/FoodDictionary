using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using FoodDictionary.Services;
using FoodDictionary.Models;

namespace FoodDictionary.Pages
{
    public partial class Catalog : ContentPage
    {
        public ObservableCollection<FoodDetail> Items { get; } = new();

        private readonly DatabaseService _databaseService = new();

        public Catalog()
        {
            InitializeComponent();
            BindingContext = this;

            // set bg from app state
            var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
            if (appState != null) BackgroundColor = appState.BackgroundColor;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // reapply bg
            var appState = Application.Current?.Handler?.MauiContext?.Services.GetService<AppState>();
            if (appState != null) BackgroundColor = appState.BackgroundColor;

            // pull food items from DB and update list
            await _databaseService.InitAsync();
            Items.Clear();

            var foods = await DatabaseService.GetConnection().Table<Food>().ToListAsync();
            foreach (var f in foods)
            {
                Items.Add(new FoodDetail
                {
                    Id = f.Id,
                    Name = f.Name,
                    Serving_Size = f.Serving_Size,
                    Price = f.Price,
                    Vitamins = string.Join(", ", await _databaseService.GetVitaminsForFoodAsync(f.Id)),
                    Minerals = string.Join(", ", await _databaseService.GetMineralsForFoodAsync(f.Id)),
                    Allergens = string.Join(", ", await _databaseService.GetAllergensForFoodAsync(f.Id)),
                    Ingredients = string.Join(", ", await _databaseService.GetIngredientsForFoodAsync(f.Id)),
                    OtherFacts = string.Join(", ", await _databaseService.GetFactsForFoodAsync(f.Id)),
                });
            }
        }

        // deletes item from DB and list
        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is FoodDetail detail)
            {
                var ok = await DisplayAlert("Delete",
                    $"Remove {detail.Name} and all its data?", "Yes", "No");
                if (!ok) return;

                await _databaseService.DeleteFoodAsync(detail.Id);
                Items.Remove(detail);
            }
        }

        // live search by name
        private async void SearchTerm_TextChanged(object sender, TextChangedEventArgs e)
        {
            var q = e.NewTextValue?.ToLower() ?? "";
            await _databaseService.InitAsync();
            var foods = await DatabaseService.GetConnection().Table<Food>().ToListAsync();

            Items.Clear();
            foreach (var f in foods)
            {
                if (f.Name?.ToLower().Contains(q) ?? false)
                {
                    Items.Add(new FoodDetail
                    {
                        Id = f.Id,
                        Name = f.Name,
                        Serving_Size = f.Serving_Size,
                        Price = f.Price,
                        Vitamins = string.Join(", ", await _databaseService.GetVitaminsForFoodAsync(f.Id)),
                        Minerals = string.Join(", ", await _databaseService.GetMineralsForFoodAsync(f.Id)),
                        Allergens = string.Join(", ", await _databaseService.GetAllergensForFoodAsync(f.Id)),
                        Ingredients = string.Join(", ", await _databaseService.GetIngredientsForFoodAsync(f.Id)),
                        OtherFacts = string.Join(", ", await _databaseService.GetFactsForFoodAsync(f.Id)),
                    });
                }
            }
        }
    }
}
