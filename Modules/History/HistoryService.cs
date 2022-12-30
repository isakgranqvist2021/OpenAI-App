using MongoDB.Bson;
using MongoDB.Driver;
using OpenAIApp.Modules.Database;

namespace OpenAIApp.Modules.History;

public class HistoryService
{
    public static async Task<List<HistoryModel>?> Read()
    {
        try
        {
            var collection = Collections.GetCollection<HistoryModel>(
                Config.Collections.HistoryCollectionName
            );

            if (collection is null)
            {
                throw new Exception("Collection is null");
            }

            var history = await collection.FindAsync(new BsonDocument { });

            return history.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public static async Task Insert(HistoryModel searchResponseModel)
    {
        try
        {
            var collection = Collections.GetCollection<BsonDocument>(
                Config.Collections.HistoryCollectionName
            );

            if (collection is null)
            {
                throw new Exception("Collection is null");
            }

            var bson = searchResponseModel.ToBsonDocument();
            await collection.InsertOneAsync(bson);
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
    }
}
