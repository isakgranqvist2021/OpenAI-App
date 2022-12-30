namespace OpenAIApp.Modules.SignUp;

public class SignUpService
{
    public static void SignUp(SignUpBody signUpBody)
    {
        try
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(
                signUpBody.Password,
                salt
            );

            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
    }
}