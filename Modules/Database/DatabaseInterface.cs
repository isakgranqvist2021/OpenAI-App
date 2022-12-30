using MongoDB.Driver;

namespace OpenAIApp.Modules.Database;

public interface DatabaseInterface
{
    public IMongoCollection<T>? GetCollection<T>(string collectionName);
}