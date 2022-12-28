using RecGen.Models;

namespace RecGen.Services;

public class Sorter
{
    public static List<SearchResponse>? sortSearchResponses(List<SearchResponse>? searchResponses)
    {
        if (searchResponses?.Count() is 0)
        {
            return null;
        }

        if (searchResponses is not null)
        {
            searchResponses.Sort(delegate (SearchResponse x, SearchResponse y)
            {
                if (y.created > x.created)
                {
                    return 1;
                }

                return -1;
            });
        }

        return searchResponses;
    }
}