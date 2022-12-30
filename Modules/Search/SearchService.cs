using System.Text;
using System.Text.Json;
using OpenAIApp.Modules.History;

namespace OpenAIApp.Modules.Search;

public class SearchService : SearchInterface
{
    private HttpClient? _client;
    private HistoryService _historyService;

    private const string baseUrl = "https://api.openai.com/v1/completions";
    public SearchService()
    {
        _client = new HttpClient();
        _historyService = new HistoryService();

        var openApiSecret = System.Environment.GetEnvironmentVariable("OPEN_API_SECRET");
        var bearerTokenHeader = String.Format("Bearer {0}", openApiSecret);
        _client.DefaultRequestHeaders.Add("Authorization", bearerTokenHeader);
    }

    public async Task<HistoryModel?> Search(string searchString)
    {
        try
        {
            if (_client is null)
            {
                throw new Exception("Http Client is null");
            }

            var payload = new SearchPayload
            {
                Model = "text-davinci-003",
                Prompt = searchString,
                Temperature = .5f,
                MaxTokens = 100,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync(baseUrl, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (responseBody is null)
            {
                throw new Exception("Response body is null");
            }

            var searchResponse = JsonSerializer.Deserialize<HistoryModel>(responseBody);

            if (searchResponse is null)
            {
                throw new Exception("Could not deserialize search response");
            }

            searchResponse.SearchString = searchString;

            await _historyService.InsertOne(searchResponse);

            return searchResponse;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}

