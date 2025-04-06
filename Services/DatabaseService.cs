using SQLite;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

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
			// Update this to your full embedded resource name
			var resourceName = "FoodDictionary.Services.FoodDictionarydb.db";

			var assembly = Assembly.GetExecutingAssembly();
			using Stream resourceStream = assembly.GetManifestResourceStream(resourceName)
				?? throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");

			using FileStream fileStream = File.Create(dbPath);
			await resourceStream.CopyToAsync(fileStream);
		}

		_database = new SQLiteAsyncConnection(dbPath);
	}

	public static SQLiteAsyncConnection GetConnection()
	{
		return _database;
	}
}