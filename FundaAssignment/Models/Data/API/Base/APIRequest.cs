using FundaAssignment.Models.Data.API.Abstract;
using System.Collections.Generic;

namespace FundaAssignment.Models.Data.API.Base
{
    public class APIRequest : IAPIRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public List<string> SearchFilters { get; set; }

    }
}
