using OpenAIApp.Modules.History;

namespace OpenAIApp.Modules.Search;

public interface SearchInterface
{
    public Task<HistoryModel?> Search(string searchString);
}