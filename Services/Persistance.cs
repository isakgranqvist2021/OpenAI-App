using MongoDB.Driver;
using OpenAIApp.Models;
using MongoDB.Bson;

namespace OpenAIApp.Services;

public class Persistance
{

    private static IMongoCollection<SearchResponse>? getCollection()
    {
        try
        {
            var client = new MongoClient(
                System.Environment.GetEnvironmentVariable("MONGODB_URI")
            );

            var database = client.GetDatabase("OpenAIApp");
            return database.GetCollection<SearchResponse>("history");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public static async Task<List<SearchResponse>?> Read()
    {
        try
        {
            var collection = getCollection();

            if (collection is null)
            {
                throw new Exception("Collection is null");
            }

            var searchResponses = await collection.FindAsync(new BsonDocument { });
            return searchResponses.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public static async Task Insert(SearchResponse searchResponse)
    {
        try
        {
            var collection = getCollection();

            if (collection is null)
            {
                throw new Exception("Collection is null");
            }

            await collection.InsertOneAsync(searchResponse);
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
    }

    public void Delete()
    {

    }
}