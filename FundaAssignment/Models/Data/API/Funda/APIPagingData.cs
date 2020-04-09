using FundaAssignment.Models.Data.API.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaAssignment.Models.Data.API.Funda
{
    public class APIPagingData : IAPIPagingData
    {
        [JsonProperty("AantalPaginas")]
        public int TotalPages { get; set; }
        [JsonProperty("HuidigePagina")]
        public int CurrentPage { get; set; }
        [JsonProperty("VolgendeUrl")]
        public string NextUrl { get; set; }
    }
}
