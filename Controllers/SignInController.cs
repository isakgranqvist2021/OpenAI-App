using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;
using OpenAIApp.Modules.SignIn;

namespace OpenAIApp.Controllers;

[Route("/sign-in")]
public class SignInController : Controller
{

    SignInService _signInService;

    public SignInController()
    {
        _signInService = new SignInService();
    }


    [HttpGet]
    public IActionResult Get()
    {
        return View(ViewPaths.SignInView);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] SignInBody signInBody)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid model state");
            }

            var user = await _signInService.SignIn(signInBody);

            if (user is null)
            {
                throw new Exception("Invalid password");
            }

            return View(ViewPaths.SignInView);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Redirect("/sign-in");
        }
    }
}
