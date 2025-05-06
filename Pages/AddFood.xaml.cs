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
	{   // Reference to the database service for handling SQLite operations
		private readonly DatabaseService _databaseService;

		private List<string> _allergens = new();
		private List<string> _ingredients = new();
		private List<string> _facts = new();

		// Constructor that receives a database service instance
		public AddFood(DatabaseService databaseService)
		{
			InitializeComponent();
			_databaseService = databaseService;
		}

		private FileResult _selectedImage;
		private async void OnAddFoodClicked(object sender, EventArgs e)
		{
			await _databaseService.InitAsync();

			// Read and clean input values from UI
			string name = NameEntry.Text?.Trim();
			string serving = ServingSizeEntry.Text?.Trim();
			string priceText = PriceEntry.Text?.Trim();
			decimal? price = null;

			// parse price input to decimal
			if (decimal.TryParse(priceText, out decimal parsedPrice))
			{
				price = parsedPrice;
			}

			// Validation
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(serving))
			{
				await DisplayAlert("Missing Info", "Name and serving size are required.", "OK");
				return;
			}

			// Create new Food object from user input
			var newFood = new Food
			{
				Name = name,
				Serving_Size = serving,
				Price = price,
				Image = _selectedImage?.FullPath
			};

			try
			{
				// Save the base food item to the database
				await _databaseService.AddFoodAsync(newFood);

				// Add selected vitamins using checkboxes
				var vitamins = new[] { VitaminA, VitaminB, VitaminC, VitaminD, VitaminE };
				var vitaminNames = new[] { "A", "B", "C", "D", "E" };

				for (int i = 0; i < vitamins.Length; i++)
				{
					if (vitamins[i].IsChecked)
					{
						await _databaseService.AddVitaminToFoodAsync(newFood.Id, vitaminNames[i]);
					}
				}

				// Add selected minerals using checkboxes
				var minerals = new[] { Calcium, Chloride, Magnesium, Phosphorus, Potassium, Sodium, Sulfur };
				var mineralNames = new[] { "Calcium", "Chloride", "Magnesium", "Phosphorus", "Potassium", "Sodium", "Sulfur" };

				for (int i = 0; i < minerals.Length; i++)
				{
					if (minerals[i].IsChecked)
					{
						await _databaseService.AddMineralToFoodAsync(newFood.Id, mineralNames[i]);
					}
				}

				// Add each entered allergen
				foreach (var allergen in _allergens)
				{
					await _databaseService.AddAllergenToFoodAsync(newFood.Id, allergen);
				}

				// Add each entered ingredient
				foreach (var ingredient in _ingredients)
				{
					await _databaseService.AddIngredientToFoodAsync(newFood.Id, ingredient);
				}

				// Add each entered other fact
				foreach (var fact in _facts)
				{
					await _databaseService.AddFactToFoodAsync(newFood.Id, fact);
				}

				await DisplayAlert("Success", "Food added successfully!", "OK");
				ClearForm();
			}
			catch (Exception ex)
			{   // Verification
				await DisplayAlert("Error", $"Failed to add food: {ex.Message}", "OK");
			}
		}

		// Clears all form inputs and resets UI state
		private void ClearForm()
		{
			NameEntry.Text = "";
			ServingSizeEntry.Text = "";
			PriceEntry.Text = "";


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

		// Adds a new allergen to the list
		private void OnAddAllergenClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(AllergenEntry.Text))
			{
				_allergens.Add(AllergenEntry.Text.Trim());
				AllergenEntry.Text = "";
			}
		}
		// Removes the last allergen added
		private void OnRemoveAllergenClicked(object sender, EventArgs e)
		{
			if (_allergens.Any())
				_allergens.RemoveAt(_allergens.Count - 1);
		}
		// Adds a new ingredient to the list
		private void OnAddIngredientClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(IngredientEntry.Text))
			{
				_ingredients.Add(IngredientEntry.Text.Trim());
				IngredientEntry.Text = "";
			}
		}

		// Removes the last ingredient added
		private void OnRemoveIngredientClicked(object sender, EventArgs e)
		{
			if (_ingredients.Any())
				_ingredients.RemoveAt(_ingredients.Count - 1);
		}

		// Adds a new fact to the list
		private void OnAddFactClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(FactEntry.Text))
			{
				_facts.Add(FactEntry.Text.Trim());
				FactEntry.Text = "";
			}
		}

		// Removes the last fact added
		private void OnRemoveFactClicked(object sender, EventArgs e)
		{
			if (_facts.Any())
				_facts.RemoveAt(_facts.Count - 1);
		}

		// Handles image selection from the device
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