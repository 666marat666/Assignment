namespace FundaAssignment.Models.Data.API.Abstract
{
    public interface IAPIPagingData
    {
        int CurrentPage { get; set; }
        string NextUrl { get; set; }
        int TotalPages { get; set; }
    }
}