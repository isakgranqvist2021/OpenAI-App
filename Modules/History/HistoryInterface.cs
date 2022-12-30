using MongoDB.Bson;

namespace OpenAIApp.Modules.History;

public interface HistoryInterface
{
    public Task<List<HistoryModel>?> Read();
    public Task<ObjectId?> InsertOne(HistoryModel searchResponseModel);
}