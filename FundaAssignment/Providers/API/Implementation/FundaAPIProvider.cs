using FundaAssignment.Models.Config.Implementation;
using FundaAssignment.Models.Data.API.Abstract;
using FundaAssignment.Models.Data.API.Base;
using FundaAssignment.Models.Data.API.Funda;
using FundaAssignment.Providers.API.Abstract;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace FundaAssignment.Providers.API.Implementation
{
    public class FundaAPIProvider : IAPIProvider
    {
        private readonly string _providerName = "Funda";
        private const int _maxRequestsCap = 65000; // 60 seconds per ~95 request
        private APIProviderSettings _apiSettings { get; set; }
        private readonly IHttpClientFactory _clientFactory;
        public FundaAPIProvider(IOptions<APIConfigRoot> apiSettings, IHttpClientFactory clientFactory)
        {
            _apiSettings = apiSettings.Value.Providers.Where(x => String.Compare(x.Name, this._providerName, StringComparison.InvariantCultureIgnoreCase) == 0).FirstOrDefault();
            if (_apiSettings == null) throw new ArgumentNullException("Api Settings doesn't contain settings for provider.");

            _clientFactory = clientFactory;
        }

        IAPIResponse HandleResponse(HttpResponseMessage httpResp)
        {
            APIResponse result = new APIResponse() { Status = new APIReturnStatus() { Type = APIReturnStatusType.None } };

            if (httpResp.IsSuccessStatusCode)
            {
                var responseStr = httpResp.Content.ReadAsStringAsync();
                //TODO: make it async
                responseStr.Wait();
                result = JsonConvert.DeserializeObject<APIResponse>(responseStr.Result);
                result.Status = new APIReturnStatus() { Type = APIReturnStatusType.Success };
            }            
            else if(httpResp.StatusCode == System.Net.HttpStatusCode.Unauthorized && httpResp.ReasonPhrase.ToLowerInvariant() == "request limit exceeded")//normally should be different status code
            {
                result.Status.Type = APIReturnStatusType.LimitExceeeded;
                result.Status.ErrorMessage = $"Cant request base service - HTTP: {httpResp.StatusCode}";
            }
            else
            {
                result.Status.Type = APIReturnStatusType.Error;
                result.Status.ErrorMessage = $"Cant request base service - HTTP: {httpResp.StatusCode} - {httpResp.ReasonPhrase}";
            }

            return result;
        }
        public IEnumerable<IAPIResponse> Call(IAPIRequest request)
        {
            //Stopwatch stopWatch = new Stopwatch();
            var pageCounter = request.Page;
            var canRun = true;
            var client = _clientFactory.CreateClient();

            var requestUrl = $"{_apiSettings.ApiUrl}/{_apiSettings.ApiKey}/?type={RequestType.Koop.ToString().ToLower()}";

            if (request.SearchFilters != null && request.SearchFilters.Count > 0)
            {
                requestUrl = $"{requestUrl}&zo=/{string.Join("/", request.SearchFilters)}";
            }
            requestUrl = $"{requestUrl}&pagesize={request.PageSize}";

            var uriBuilder = new UriBuilder(requestUrl);
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{requestUrl}&page={pageCounter}");
            //stopWatch.Start();
            do
            {
                //TODO: make it async
                var task = client.SendAsync(httpRequest);
                task.Wait();
                var response = task.Result;

                var result = HandleResponse(response);
                if (result.Status.Type == APIReturnStatusType.Success)
                {
                    yield return result;
                }                
                else if(result.Status.Type == APIReturnStatusType.Error)
                {
                    throw new Exception(result.Status.ErrorMessage);
                }
                
                if(result.Status.Type == APIReturnStatusType.LimitExceeeded)
                {
                    Thread.Sleep(_maxRequestsCap);
                    httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{requestUrl}&page={pageCounter}");
                    canRun = true;
                }
                else if(result?.Paging?.TotalPages > pageCounter)
                {
                    pageCounter += 1;
                    httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{requestUrl}&page={pageCounter}");
                    canRun = true;
                }
                else
                {
                    canRun = false;
                }

                
                //if (pageCounter % 95 == 0 && stopWatch.ElapsedMilliseconds < 60000) //// 60 seconds per ~95 request
                //{
                //    Thread.Sleep(_maxRequestsCap - (int)stopWatch.ElapsedMilliseconds);
                //    stopWatch.Reset();
                //}
                    
            } while (canRun);
        }

    }
}
