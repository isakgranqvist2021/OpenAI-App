using MongoDB.Bson;
using MongoDB.Driver;
using OpenAIApp.Modules.Database;

namespace OpenAIApp.Modules.History;

public class HistoryService : HistoryInterface
{

    private Collections _collections;

    public HistoryService()
    {
        _collections = new Collections();
    }

    public async Task<List<HistoryModel>?> Read()
    {
        try
        {
            var collection = _collections.GetCollection<HistoryModel>(
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

    public async Task Insert(HistoryModel searchResponseModel)
    {
        try
        {
            var collection = _collections.GetCollection<BsonDocument>(
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
