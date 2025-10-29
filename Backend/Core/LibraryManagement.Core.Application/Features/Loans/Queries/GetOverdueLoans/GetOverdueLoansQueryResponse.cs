using System;

namespace LibraryManagement.Core.Application.Features.Loans.Queries.GetOverdueLoans
{
    public class GetOverdueLoansQueryResponse
    {
        public int Id { get; set; }

        // Book info
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string ISBN { get; set; }

        // Member info
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberCode { get; set; }

        // Loan info
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public int DaysOverdue => (DateTime.UtcNow - DueDate).Days;
    }
}
