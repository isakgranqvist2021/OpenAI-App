using MongoDB.Bson;

namespace OpenAIApp.Modules.History;

public interface HistoryInterface
{
    public Task<List<HistoryModel>?> Read(ObjectId userId);
    public Task<ObjectId?> InsertOne(HistoryModel searchResponseModel);
}