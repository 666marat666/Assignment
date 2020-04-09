using System;

namespace FundaAssignment.Models.Data.API.Abstract
{
    public interface IAPIAd
    {
        int BrokerId { get; set; }
        string BrokerName { get; set; }
        string Id { get; set; }
        bool IsRented { get; set; }
        bool IsSearchable { get; set; }
        bool IsSold { get; set; }
        bool IsSoldOrLeased { get; set; }
        DateTime? PublicationDate { get; set; }
    }
}