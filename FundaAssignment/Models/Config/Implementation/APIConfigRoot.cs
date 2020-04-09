using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaAssignment.Models.Config.Implementation
{
    public class APIConfigRoot
    {
        public List<APIProviderSettings> Providers { get; set; }
    }
}
