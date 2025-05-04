using FoodDictionary.Models;
using FoodDictionary.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDictionary.Pages
{
	public partial class AddFood : ContentPage
	{
		private readonly DatabaseService _databaseService;

		private List<string> _allergens = new();
		private List<string> _ingredients = new();
		private List<string> _facts = new();

		public AddFood(DatabaseService databaseService)
		{
			InitializeComponent();
			_databaseService = databaseService;
		}

		private FileResult _selectedImage;
		private async void OnAddFoodClicked(object sender, EventArgs e)
		{
			await _databaseService.InitAsync();

			string name = NameEntry.Text?.Trim();
			string serving = ServingSizeEntry.Text?.Trim();
			string priceText = PriceEntry.Text?.Trim();
			decimal? price = null;
			if (decimal.TryParse(priceText, out decimal parsedPrice))
			{
				price = parsedPrice;
			}

			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(serving))
			{
				await DisplayAlert("Missing Info", "Name and serving size are required.", "OK");
				return;
			}

			var newFood = new Food
			{
				Name = name,
				Serving_Size = serving,
				Price = price,
				Image = _selectedImage?.FullPath
			};

			try
			{
				await _databaseService.AddFoodAsync(newFood);


				var vitamins = new[] { VitaminA, VitaminB, VitaminC, VitaminD, VitaminE };
				var vitaminNames = new[] { "A", "B", "C", "D", "E" };

				for (int i = 0; i < vitamins.Length; i++)
				{
					if (vitamins[i].IsChecked)
					{
						await _databaseService.AddVitaminToFoodAsync(newFood.Id, vitaminNames[i]);
					}
				}


				var minerals = new[] { Calcium, Chloride, Magnesium, Phosphorus, Potassium, Sodium, Sulfur };
				var mineralNames = new[] { "Calcium", "Chloride", "Magnesium", "Phosphorus", "Potassium", "Sodium", "Sulfur" };

				for (int i = 0; i < minerals.Length; i++)
				{
					if (minerals[i].IsChecked)
					{
						await _databaseService.AddMineralToFoodAsync(newFood.Id, mineralNames[i]);
					}
				}


				foreach (var allergen in _allergens)
				{
					await _databaseService.AddAllergenToFoodAsync(newFood.Id, allergen);
				}


				foreach (var ingredient in _ingredients)
				{
					await _databaseService.AddIngredientToFoodAsync(newFood.Id, ingredient);
				}


				foreach (var fact in _facts)
				{
					await _databaseService.AddFactToFoodAsync(newFood.Id, fact);
				}

				await DisplayAlert("Success", "Food added successfully!", "OK");
				ClearForm();
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", $"Failed to add food: {ex.Message}", "OK");
			}
		}

		private void ClearForm()
		{
			NameEntry.Text = "";
			ServingSizeEntry.Text = "";
			VitaminA.IsChecked = false;
			VitaminB.IsChecked = false;
			VitaminC.IsChecked = false;
			VitaminD.IsChecked = false;
			VitaminE.IsChecked = false;
			Calcium.IsChecked = false;
			Chloride.IsChecked = false;
			Magnesium.IsChecked = false;
			Phosphorus.IsChecked = false;
			Potassium.IsChecked = false;
			Sodium.IsChecked = false;
			Sulfur.IsChecked = false;

			_allergens.Clear();
			_ingredients.Clear();
			_facts.Clear();
		}

		private void OnAddAllergenClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(AllergenEntry.Text))
			{
				_allergens.Add(AllergenEntry.Text.Trim());
				AllergenEntry.Text = "";
			}
		}

		private void OnRemoveAllergenClicked(object sender, EventArgs e)
		{
			if (_allergens.Any())
				_allergens.RemoveAt(_allergens.Count - 1);
		}

		private void OnAddIngredientClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(IngredientEntry.Text))
			{
				_ingredients.Add(IngredientEntry.Text.Trim());
				IngredientEntry.Text = "";
			}
		}

		private void OnRemoveIngredientClicked(object sender, EventArgs e)
		{
			if (_ingredients.Any())
				_ingredients.RemoveAt(_ingredients.Count - 1);
		}

		private void OnAddFactClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(FactEntry.Text))
			{
				_facts.Add(FactEntry.Text.Trim());
				FactEntry.Text = "";
			}
		}

		private void OnRemoveFactClicked(object sender, EventArgs e)
		{
			if (_facts.Any())
				_facts.RemoveAt(_facts.Count - 1);
		}

		private async void OnPickImageClicked(object sender, EventArgs e)
		{
			try
			{
				_selectedImage = await MediaPicker.PickPhotoAsync();

				if (_selectedImage != null)
				{
					var stream = await _selectedImage.OpenReadAsync();
					FoodImage.Source = ImageSource.FromStream(() => stream);
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", $"Image selection failed: {ex.Message}", "OK");
			}
		}
	}
}