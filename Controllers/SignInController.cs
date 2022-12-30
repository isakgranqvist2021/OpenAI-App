using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;
using OpenAIApp.Modules.SignIn;

namespace OpenAIApp.Controllers;

[Route("/sign-in")]
public class SignInController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return View(ViewPaths.SignInView);
    }

    [HttpPost]
    public IActionResult Post([FromForm] SignInBody signInBody)
    {
        return View(ViewPaths.SignInView);
    }
}
