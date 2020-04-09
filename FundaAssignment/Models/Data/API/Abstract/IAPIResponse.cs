using FundaAssignment.Models.Data.API.Base;
using FundaAssignment.Models.Data.API.Funda;
using System.Collections.Generic;

namespace FundaAssignment.Models.Data.API.Abstract
{
    public interface IAPIResponse
    {
        IEnumerable<APIAd> Adverts { get; set; }
        APIPagingData Paging { get; set; }
        int TotalObjects { get; set; }
        APIReturnStatus Status { get; set; }
    }
}