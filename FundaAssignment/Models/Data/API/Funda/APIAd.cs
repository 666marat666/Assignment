using FundaAssignment.Models.Data.API.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaAssignment.Models.Data.API.Funda
{
    public class APIAd : IAPIAd
    {
        public string Id { get; set; }
        public bool IsSearchable { get; set; }
        [JsonProperty("IsVerhuurd")]
        public bool IsRented { get; set; }
        [JsonProperty("IsVerkocht")]
        public bool IsSold { get; set; }
        [JsonProperty("IsVerkochtOfVerhuurd")]
        public bool IsSoldOrLeased { get; set; }
        [JsonProperty("MakelaarId")]
        public int BrokerId { get; set; }
        [JsonProperty("MakelaarNaam")]
        public string BrokerName { get; set; }
        [JsonProperty("PublicatieDatum")]
        public DateTime? PublicationDate { get; set; }
    }
}
