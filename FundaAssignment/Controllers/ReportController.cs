using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundaAssignment.Models.Config.Implementation;
using FundaAssignment.Models.Data.API.Abstract;
using FundaAssignment.Models.Data.API.Base;
using FundaAssignment.Models.Data.API.Funda;
using FundaAssignment.Providers.API.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FundaAssignment.Controllers
{
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly ILogger<ReportController> _logger;
        private readonly IAPIProvider _apiProvider;

        public ReportController(ILogger<ReportController> logger, IAPIProvider apiProvider)
        {
            _logger = logger;
            _apiProvider = apiProvider;

        }


        [HttpGet]
        [Route("[controller]/test")]
        public IEnumerable<string> Test()
        {
            return new List<string>() { "1","2","3" };
        }

        [HttpGet]
        [Route("[controller]/all")]
        public IAPIResponse GetAll()
        {
            return  _apiProvider.Call(new APIRequest()).FirstOrDefault();
        }

        [HttpGet]
        [Route("[controller]/top/{filters}")]
        public object GetSpecific(string filters)
        {
            return _apiProvider.Call(
                new APIRequest()
                {
                    //SearchFilters = new List<string> { "amsterdam", "tuin" }
                    SearchFilters = filters.Split(",").ToList()
                })
                .SelectMany(x => x.Adverts.Select(y => new { BrokerID = y?.BrokerId, BrokerName = y?.BrokerName, Page = x?.Paging?.CurrentPage }))
                .GroupBy(x => x.BrokerName)
                .Select(x => new { Name = x.Key, Count = x.Count() })
                .OrderBy(x => x.Count).Reverse();

        }
    }
}
