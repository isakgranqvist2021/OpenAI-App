using System.ComponentModel.DataAnnotations;

namespace OpenAIApp.Modules.SignIn;

public class SignInBody
{
    [Required, Range(1, 99)]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required, Range(1, 99)]
    public string? Password { get; set; }
}