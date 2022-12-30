using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;
using OpenAIApp.Modules.SignUp;

namespace OpenAIApp.Controllers;

[Route("/sign-up")]
public class SignUpController : Controller
{

    SignUpService _signUpService;

    public SignUpController()
    {
        _signUpService = new SignUpService();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return View(ViewPaths.SignUpView);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] SignUpBody signUpBody)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Model state invalid");
            }

            await _signUpService.SignUp(signUpBody);
            return View(ViewPaths.SignUpView);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Redirect("/sign-up");
        }
    }
}
