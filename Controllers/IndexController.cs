using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;
using OpenAIApp.Modules.Search;
using OpenAIApp.Modules.History;

namespace OpenAIApp.Controllers;

public class IndexTemplatePayload
{
    public List<HistoryModel>? SearchHistory { get; set; } = null;
    public HistoryModel? SearchResponse { get; set; } = null;
}

[Route("/")]
public class IndexController : Controller
{
    private readonly SearchService _searchService = new SearchService();

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var history = Sorter.sortSearchResponseModels(await HistoryService.Read());

        return View(ViewPaths.IndexView, new IndexTemplatePayload
        {
            SearchHistory = history,
            SearchResponse = null
        });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] SearchBody data)
    {
        try
        {
            var HistoryModel = await _searchService.Search(data.SearchString);
            var history = Sorter.sortSearchResponseModels(await HistoryService.Read());

            return View(ViewPaths.IndexView, new IndexTemplatePayload
            {
                SearchHistory = history,
                SearchResponse = HistoryModel
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Redirect("/");
        }
    }
}
