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
        public async Task DeleteRelatedInfoAsync(int foodId)
        {
            await InitAsync();

            var foodVitamins = await _database.Table<FoodVitamin>()
                                               .Where(fv => fv.FoodId == foodId)
                                               .ToListAsync();
            foreach (var fv in foodVitamins)
                await _database.DeleteAsync(fv);

            var foodMinerals = await _database.Table<Foodmineral>()
                                               .Where(fm => fm.FoodId == foodId)
                                               .ToListAsync();
            foreach (var fm in foodMinerals)
                await _database.DeleteAsync(fm);

            var allergens = await _database.Table<Allergen>()
                                           .Where(a => a.FoodId == foodId)
                                           .ToListAsync();
            foreach (var a in allergens)
                await _database.DeleteAsync(a);

            var ingredients = await _database.Table<Ingredient>()
                                             .Where(i => i.FoodId == foodId)
                                             .ToListAsync();
            foreach (var i in ingredients)
                await _database.DeleteAsync(i);

            var facts = await _database.Table<OtherFact>()
                                       .Where(f => f.FoodId == foodId)
                                       .ToListAsync();
            foreach (var f in facts)
                await _database.DeleteAsync(f);
        }

        public async Task DeleteFoodAsync(int foodId)
        {
            await InitAsync();

            await DeleteRelatedInfoAsync(foodId);

            await _database.DeleteAsync(new Food { Id = foodId });
        }
        public async Task<List<string>> GetVitaminsForFoodAsync(int foodId)
        {
            var fvs = await _database.Table<FoodVitamin>()
                                     .Where(fv => fv.FoodId == foodId)
                                     .ToListAsync();

            var names = new List<string>();
            foreach (var fv in fvs)
            {
                var v = await _database.Table<Vitamin>()
                                       .Where(x => x.Id == fv.VitaminId)
                                       .FirstOrDefaultAsync();
                if (v != null) names.Add(v.Name);
            }
            return names;
        }

        public async Task<List<string>> GetMineralsForFoodAsync(int foodId)
        {
            var fms = await _database.Table<Foodmineral>()
                                     .Where(fm => fm.FoodId == foodId)
                                     .ToListAsync();

            var names = new List<string>();
            foreach (var fm in fms)
            {
                var m = await _database.Table<Mineral>()
                                       .Where(x => x.Id == fm.MineralId)
                                       .FirstOrDefaultAsync();
                if (m != null) names.Add(m.Name);
            }
            return names;
        }

        public async Task<List<string>> GetAllergensForFoodAsync(int foodId)
        {
            var alls = await _database.Table<Allergen>()
                                      .Where(a => a.FoodId == foodId)
                                      .ToListAsync();
            return alls.Select(a => a.Name).ToList();
        }

        public async Task<List<string>> GetIngredientsForFoodAsync(int foodId)
        {
            var ings = await _database.Table<Ingredient>()
                                      .Where(i => i.FoodId == foodId)
                                      .ToListAsync();
            return ings.Select(i => i.Name).ToList();
        }

        public async Task<List<string>> GetFactsForFoodAsync(int foodId)
        {
            var facts = await _database.Table<OtherFact>()
                                       .Where(f => f.FoodId == foodId)
                                       .ToListAsync();
            return facts.Select(f => f.Fact).ToList();
        }
    }
}
