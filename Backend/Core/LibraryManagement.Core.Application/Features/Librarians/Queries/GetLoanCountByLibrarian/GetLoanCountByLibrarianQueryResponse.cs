namespace LibraryManagement.Core.Application.Features.Librarians.Queries.GetLoanCountByLibrarian
{
    public class GetLoanCountByLibrarianQueryResponse
    {
        public int LibrarianId { get; set; }
        public string LibrarianName { get; set; }
        public int LoanCount { get; set; }
    }
}
