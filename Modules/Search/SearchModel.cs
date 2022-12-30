using System.Text.Json.Serialization;

namespace OpenAIApp.Modules.Search;

public class SearchBody
{
    public string SearchString { get; set; } = "";
}

public class SearchPayload
{

    [JsonPropertyName("model")]
    public string Model { get; set; } = "";

    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = "";

    [JsonPropertyName("temperature")]
    public float Temperature { get; set; } = 0;

    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; } = 7;
}

