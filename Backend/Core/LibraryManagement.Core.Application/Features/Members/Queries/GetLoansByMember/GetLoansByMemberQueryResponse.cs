using System;

namespace LibraryManagement.Core.Application.Features.Members.Queries.GetLoansByMember
{
    public class GetLoansByMemberQueryResponse
    {
        public string BookTitle { get; set; }
        public string ISBN { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
