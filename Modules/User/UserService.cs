using MongoDB.Bson;
using MongoDB.Driver;
using OpenAIApp.Modules.Database;

namespace OpenAIApp.Modules.User;

public class UserService
{
    private DatabaseService _databaseService;

    public UserService()
    {
        _databaseService = new DatabaseService();
    }

    public async Task<UserModel?> ReadOneByEmail(string Email)
    {
        try
        {
            var collection = _databaseService.GetCollection<UserModel>(DatabaseCollections.UsersCollectionName);

            if (collection is null)
            {
                throw new Exception("Collection is null");
            }


            return (await collection.FindAsync(new BsonDocument
            { ["email"] = Email })).First();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<ObjectId?> InsertOne(UserModel userModel)
    {
        try
        {
            var collection = _databaseService.GetCollection<UserModel>(DatabaseCollections.UsersCollectionName);

            if (collection is null)
            {
                throw new Exception("Collection is null");
            }

            await collection.InsertOneAsync(userModel);

            return userModel.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}