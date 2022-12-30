namespace OpenAIApp.Modules.SignIn;

public class SignInService
{

    public static void SignIn(SignInBody signInBody)
    {
        try
        {
            var hash = "abcdfg";
            var ok = BCrypt.Net.BCrypt.Verify(signInBody.Password, hash);

            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
    }
}