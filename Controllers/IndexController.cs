using Microsoft.AspNetCore.Mvc;
using RecGen.Config;
using RecGen.Models;
using RecGen.Services;

namespace RecGen.Controllers;

[Route("/")]
public class IndexController : Controller
{
    private readonly SearchService _searchService = new SearchService();

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var history = await Persistance.Read();

        if (history?.Count() is 0)
        {
            history = null;
        }

        return View(ViewPaths.IndexView, new TemplatePayload
        {
            searchHistory = history,
            searchResponse = null
        });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] SearchModel data)
    {
        var searchResponse = await _searchService.Search(data.SearchString);
        var history = await Persistance.Read();

        if (history?.Count() is 0)
        {
            history = null;
        }

        return View(ViewPaths.IndexView, new TemplatePayload
        {
            searchHistory = history,
            searchResponse = searchResponse
        });
    }
}
