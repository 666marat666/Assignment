using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaAssignment.Models.Data.API.Base
{
    public enum APIReturnStatusType
    {
        None,
        Error,
        LimitExceeeded,
        Success
    }

    public class APIReturnStatus
    {
        public APIReturnStatusType Type { get; set; }
        public string ErrorMessage { get; set; }
    }
}
