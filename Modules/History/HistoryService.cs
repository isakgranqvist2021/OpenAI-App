using MongoDB.Bson;
using MongoDB.Driver;
using OpenAIApp.Modules.Database;

namespace OpenAIApp.Modules.History;

public class HistoryService : HistoryInterface
{
    private DatabaseService _databaseService;

    public HistoryService()
    {
        _databaseService = new DatabaseService();
    }

    public async Task<List<HistoryModel>?> Read()
    {
        try
        {
            var collection = _databaseService.GetCollection<HistoryModel>(
                DatabaseCollections.HistoryCollectionName
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

    public async Task<ObjectId?> InsertOne(HistoryModel searchResponseModel)
    {
        try
        {
            searchResponseModel.Id = ObjectId.GenerateNewId();

            var collection = _databaseService.GetCollection<BsonDocument>(
                DatabaseCollections.HistoryCollectionName
            );

            if (collection is null)
            {
                throw new Exception("Collection is null");
            }

            await collection.InsertOneAsync(searchResponseModel.ToBsonDocument());
            return searchResponseModel.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
