using RecGen.Models;
using System.Text;
using System.Text.Json;

namespace RecGen.Services;

public class SearchService
{
    private static HttpClient? _client;

    private const string baseUrl = "https://api.openai.com/v1/completions";

    public SearchService()
    {
        _client = new HttpClient();

        var openApiSecret = System.Environment.GetEnvironmentVariable("OPEN_API_SECRET");
        var bearerTokenHeader = String.Format("Bearer {0}", openApiSecret);
        _client.DefaultRequestHeaders.Add("Authorization", bearerTokenHeader);
    }

    public async Task<SearchResponse?> Search(string searchString)
    {
        try
        {
            if (_client is null)
            {
                throw new Exception("Http Client is null");
            }

            var payload = new SearchPayload
            {
                model = "text-davinci-003",
                prompt = searchString,
                temperature = .5f,
                max_tokens = 100,
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

            var searchResponse = JsonSerializer.Deserialize<SearchResponse>(responseBody);

            if (searchResponse is null)
            {
                throw new Exception("Could not deserialize search response");
            }

            return searchResponse;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}