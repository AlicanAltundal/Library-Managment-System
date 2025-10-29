namespace LibraryManagement.Core.Application.Features.Stats.Queries.GetDashboardData
{
    public class GetDashboardDataQueryResponse
    {
        public int TotalBooks { get; set; }     
        public int TotalMembers { get; set; }    
        public int TotalLoans { get; set; }

        public int TotalPublishers { get; set; }
        public int OverdueCount { get; set; }  
        public int ActiveLoans { get; set; }     
        public int ReturnedLoans { get; set; }  
    }
}
