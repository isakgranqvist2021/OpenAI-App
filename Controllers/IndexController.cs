using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;
using OpenAIApp.Models;
using OpenAIApp.Services;
namespace OpenAIApp.Controllers;

[Route("/")]
public class IndexController : Controller
{
    private readonly SearchService _searchService = new SearchService();

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var history = Sorter.sortSearchResponses(await HistoryService.Read());

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
        var history = Sorter.sortSearchResponses(await HistoryService.Read());

        return View(ViewPaths.IndexView, new TemplatePayload
        {
            searchHistory = history,
            searchResponse = searchResponse
        });
    }
}
