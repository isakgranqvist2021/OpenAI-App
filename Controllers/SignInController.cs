using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;

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
    public IActionResult Post()
    {
        return View(ViewPaths.SignInView);
    }
}
