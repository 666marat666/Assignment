using FundaAssignment.Models.Data.API.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundaAssignment.Providers.API.Abstract
{
    public interface IAPIProvider
    {
        IEnumerable<IAPIResponse> Call(IAPIRequest request);
    }
}