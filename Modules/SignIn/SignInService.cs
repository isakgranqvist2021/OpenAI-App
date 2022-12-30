using OpenAIApp.Modules.User;

namespace OpenAIApp.Modules.SignIn;

public class SignInService
{

    UserService _userService;

    public SignInService()
    {
        _userService = new UserService();
    }

    public async Task<UserModel?> SignIn(SignInBody signInBody)
    {
        try
        {
            if (signInBody.Email is null)
            {
                throw new Exception("Email is required");
            }

            var user = await _userService.ReadOneByEmail(signInBody.Email);

            if (user is null)
            {
                throw new Exception("No user with provided email was found");
            }

            var enteredPasswordIsCorrect = BCrypt.Net.BCrypt.Verify(
                signInBody.Password,
                user.Password
            );

            if (!enteredPasswordIsCorrect)
            {
                return null;
            }

            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}