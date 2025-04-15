using SQLite;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using FoodDictionary.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodDictionary.Services
{
	public class DatabaseService
	{
		private static SQLiteAsyncConnection _database;
		private static readonly string dbName = "FoodDictionarydb.db";

		public async Task InitAsync()
		{
			if (_database != null)
				return;

			string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbName);

			if (!File.Exists(dbPath))
			{
				var resourceName = "FoodDictionary.Services.FoodDictionarydb.db";
				var assembly = Assembly.GetExecutingAssembly();

				using Stream resourceStream = assembly.GetManifestResourceStream(resourceName)
					?? throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");

				using FileStream fileStream = File.Create(dbPath);
				await resourceStream.CopyToAsync(fileStream);
			}

			_database = new SQLiteAsyncConnection(dbPath);

			await _database.CreateTableAsync<Food>();
			await _database.CreateTableAsync<Allergen>();
			await _database.CreateTableAsync<Vitamin>();
			await _database.CreateTableAsync<FoodVitamin>();
			await _database.CreateTableAsync<Mineral>();
			await _database.CreateTableAsync<Foodmineral>();
			await _database.CreateTableAsync<Ingredient>();
			await _database.CreateTableAsync<OtherFact>();
		}

		public static SQLiteAsyncConnection GetConnection() => _database;

		public async Task<int> AddFoodAsync(Food food)
		{
			await InitAsync();
			return await _database.InsertAsync(food);
		}

		public async Task AddVitaminToFoodAsync(int foodId, string vitaminName)
		{
			var vitamin = await _database.Table<Vitamin>().Where(v => v.Name == vitaminName).FirstOrDefaultAsync();
			if (vitamin == null)
			{
				vitamin = new Vitamin { Name = vitaminName };
				await _database.InsertAsync(vitamin);
			}

			await _database.InsertAsync(new FoodVitamin { FoodId = foodId, VitaminId = vitamin.Id });
		}

		public async Task AddMineralToFoodAsync(int foodId, string mineralName)
		{
			var mineral = await _database.Table<Mineral>().Where(m => m.Name == mineralName).FirstOrDefaultAsync();
			if (mineral == null)
			{
				mineral = new Mineral { Name = mineralName };
				await _database.InsertAsync(mineral);
			}

			await _database.InsertAsync(new Foodmineral { FoodId = foodId, MineralId = mineral.Id });
		}

		public async Task AddAllergenToFoodAsync(int foodId, string allergenName)
		{
			var allergen = new Allergen { FoodId = foodId, Name = allergenName };
			await _database.InsertAsync(allergen);
		}

		public async Task AddIngredientToFoodAsync(int foodId, string ingredientName)
		{
			var ingredient = new Ingredient { FoodId = foodId, Name = ingredientName };
			await _database.InsertAsync(ingredient);
		}

		public async Task AddFactToFoodAsync(int foodId, string fact)
		{
			var otherFact = new OtherFact { FoodId = foodId, Fact = fact };
			await _database.InsertAsync(otherFact);
		}
	}
}