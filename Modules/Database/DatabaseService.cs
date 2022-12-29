using MongoDB.Driver;

namespace OpenAIApp.Modules.Database;

public class Collections
{
    public static IMongoCollection<T>? GetCollection<T>(string collectionName)
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