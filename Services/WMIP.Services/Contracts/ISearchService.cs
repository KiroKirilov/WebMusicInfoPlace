using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Services.Dtos.Search;

namespace WMIP.Services.Contracts
{
    public interface ISearchService
    {
        IEnumerable<SearchResultDto> GetResults(string searchTerm);
    }
}
