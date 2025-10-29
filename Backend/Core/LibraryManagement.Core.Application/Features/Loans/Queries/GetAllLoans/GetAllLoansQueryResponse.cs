using System;

namespace LibraryManagement.Core.Application.Features.Loans.Queries.GetAllLoans
{
    public class GetAllLoansQueryResponse
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

        // Librarian info
        public int LibrarianId { get; set; }
        public string LibrarianName { get; set; }

        // Loan info
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned => ReturnDate != null;
    }
}
