using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;
namespace OpenAIApp.Controllers;

[Route("/sign-up")]
public class SignUpController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return View(ViewPaths.SignUpView);
    }
}
