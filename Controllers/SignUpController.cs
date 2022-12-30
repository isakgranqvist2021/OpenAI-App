using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;
using OpenAIApp.Modules.SignUp;

namespace OpenAIApp.Controllers;

[Route("/sign-up")]
public class SignUpController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return View(ViewPaths.SignUpView);
    }

    [HttpPost]
    public IActionResult Post([FromForm] SignUpBody signUpBody)
    {
        return View(ViewPaths.SignUpView);
    }
}
