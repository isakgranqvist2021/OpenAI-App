using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OpenAIApp.Modules.SignIn;

public class SignInBody
{
    public string? Email { get; set; }

    public string? Password { get; set; }
}