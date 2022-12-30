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
        var id = HttpContext.Session.GetString("Session");

        if (id is not null)
        {
            return Redirect("/");
        }

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

            var id = await _signUpService.SignUp(signUpBody);

            if (id is null)
            {
                return Redirect("/sign-up");
            }

            HttpContext.Session.SetString("Session", id.ToString()!);
            return Redirect("/");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Redirect("/sign-up");
        }
    }
}
