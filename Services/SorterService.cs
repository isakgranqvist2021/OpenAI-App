using OpenAIApp.Models;

namespace OpenAIApp.Services;

public class Sorter
{
    public static List<SearchResponse>? sortSearchResponses(List<SearchResponse>? searchResponses)
    {
        if (searchResponses is null)
        {
            return null;
        }

        if (searchResponses.Count() is 0)
        {
            return null;
        }

        searchResponses.Sort(delegate (SearchResponse x, SearchResponse y)
        {
            if (y.created > x.created)
            {
                return 1;
            }

            return -1;
        });

        return searchResponses;
    }
}