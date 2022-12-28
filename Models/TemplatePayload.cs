namespace OpenAIApp.Models;

public class TemplatePayload
{
    public List<SearchResponse>? searchHistory { get; set; } = null;
    public SearchResponse? searchResponse { get; set; } = null;
}