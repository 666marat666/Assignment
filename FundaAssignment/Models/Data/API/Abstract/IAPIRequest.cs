using System.Collections.Generic;

namespace FundaAssignment.Models.Data.API.Abstract
{
    public interface IAPIRequest
    {
        int Page { get; set; }
        int PageSize { get; set; }
        List<string> SearchFilters { get; set; }
    }
}
