using MongoDB.Driver;

namespace OpenAIApp.Modules.Database;

public class Collections : DatabaseInterface
{
    public IMongoCollection<T>? GetCollection<T>(string collectionName)
    {
        try
        {
            var client = new MongoClient(
                System.Environment.GetEnvironmentVariable("MONGODB_URI")
            );

            var database = client.GetDatabase("OpenAIApp");
            return database.GetCollection<T>(collectionName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}