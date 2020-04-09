using FundaAssignment.Models.Data.API.Abstract;
using FundaAssignment.Models.Data.API.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaAssignment.Models.Data.API.Funda
{
    public class APIResponse : IAPIResponse
    {
        [JsonProperty("Objects")]
        public IEnumerable<APIAd> Adverts { get; set; }
        public APIPagingData Paging { get; set; }
        [JsonProperty("TotaalAantalObjecten")]
        public int TotalObjects { get; set; }
        public APIReturnStatus Status { get; set; }
    }
}
