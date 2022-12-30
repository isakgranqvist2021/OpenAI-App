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
    private SearchService _searchService;
    private HistoryService _historyService;

    public IndexController()
    {
        _searchService = new SearchService();
        _historyService = new HistoryService();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var history = Sorter.sortSearchResponseModels(await _historyService.Read());

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
            if (!ModelState.IsValid)
            {
                throw new Exception("Model state invalid");
            }

            var HistoryModel = await _searchService.Search(data.SearchString);
            var history = Sorter.sortSearchResponseModels(await _historyService.Read());

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
