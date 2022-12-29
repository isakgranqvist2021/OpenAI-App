using MongoDB.Driver;
using OpenAIApp.Models;
using MongoDB.Bson;
using OpenAIApp.Services.Database;

namespace OpenAIApp.Services;

public class HistoryService
{
    public static async Task<List<SearchResponse>?> Read()
    {
        try
        {
            var collection = Collections.GetCollection<SearchResponse>(
                Config.Collections.HistoryCollectionName
            );

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
            var collection = Collections.GetCollection<SearchResponse>(
                Config.Collections.HistoryCollectionName
            );

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