using OpenAIApp.Modules.User;
using MongoDB.Bson;

namespace OpenAIApp.Modules.SignUp;

public class SignUpService
{

    UserService _userService;

    public SignUpService()
    {
        _userService = new UserService();
    }

    public async Task<ObjectId?> SignUp(SignUpBody signUpBody)
    {
        try
        {
            if (signUpBody.Email is null)
            {
                throw new Exception("Email is required");
            }

            var user = await _userService.ReadOneByEmail(signUpBody.Email);

            if (user is not null)
            {
                throw new Exception("User with email already exists");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(
                signUpBody.Password,
                salt
            );

            var id = await _userService.InsertOne(new UserModel
            {
                Id = ObjectId.GenerateNewId(),
                Email = signUpBody.Email,
                Password = passwordHash
            });

            return id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}