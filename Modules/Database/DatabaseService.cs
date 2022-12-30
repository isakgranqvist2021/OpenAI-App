using MongoDB.Driver;

namespace OpenAIApp.Modules.Database;

public class DatabaseService : DatabaseInterface
{

    private IMongoDatabase _database;

    public DatabaseService()
    {
        var client = new MongoClient(
            System.Environment.GetEnvironmentVariable("MONGODB_URI")
        );

        _database = client.GetDatabase(
            System.Environment.GetEnvironmentVariable("DB_NAME")
        );
    }

    public IMongoCollection<T>? GetCollection<T>(string collectionName)
    {
        try
        {
            return _database.GetCollection<T>(collectionName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}