using Microsoft.AspNetCore.Mvc;

namespace OpenAIApp.Controllers;

[Route("/sign-out")]
public class SignOutController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        HttpContext.Session.Remove("Session");
        return Redirect("/sign-in");
    }
}