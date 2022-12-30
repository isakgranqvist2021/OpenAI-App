using System.ComponentModel.DataAnnotations;

namespace OpenAIApp.Modules.SignUp;

public class SignUpBody
{
    [Required, Range(1, 99)]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required, Range(1, 99)]
    public string? Password { get; set; }
}