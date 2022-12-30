using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace OpenAIApp.Modules.History;

public class ChoiceModel
{
    [BsonRequired]
    [BsonElement("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; } = "";

    [BsonRequired]
    [BsonElement("index")]
    [JsonPropertyName("index")]
    public int Index { get; set; } = 0;

    [BsonRequired]
    [BsonElement("logprobs")]
    [JsonPropertyName("logprobs")]
    public string? LogProbs { get; set; } = null;

    [BsonRequired]
    [BsonElement("finish_reason")]
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; } = "length";
}

public class UsageModel
{
    [BsonRequired]
    [BsonElement("prompt_tokens")]
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; } = 0;

    [BsonRequired]
    [BsonElement("completion_tokens")]
    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; } = 0;

    [BsonRequired]
    [BsonElement("total_tokens")]
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; } = 0;
}


public class HistoryModel
{
    [BsonRequired]
    [BsonIdAttribute]
    [BsonElement("id")]
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [BsonRequired]
    [BsonElement("searchString")]
    [JsonPropertyName("searchString")]
    public string SearchString { get; set; } = "";

    [BsonRequired]
    [BsonElement("obj")]
    [JsonPropertyName("obj")]
    public string Obj { get; set; } = "";

    [BsonRequired]
    [BsonElement("created")]
    [JsonPropertyName("created")]
    public long Created { get; set; } = 0;

    [BsonRequired]
    [BsonElement("model")]
    [JsonPropertyName("model")]
    public string Model { get; } = "text-davinci:003";

    [BsonRequired]
    [BsonElement("choices")]
    [JsonPropertyName("choices")]
    public List<ChoiceModel> Choices { get; set; } = new List<ChoiceModel>();

    [BsonRequired]
    [BsonElement("usage")]
    [JsonPropertyName("usages")]
    public UsageModel? Usage { get; set; } = null;
}
