using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OpenAIApp.Modules.SignUp;

public class SignUpBody
{
    public string? Email { get; set; }

    public string? Password { get; set; }
}