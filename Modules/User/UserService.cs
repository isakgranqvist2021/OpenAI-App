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
        return null;
    }

    public async Task<string?> InsertOne(UserModel userModel)
    {
        return null;
    }
}