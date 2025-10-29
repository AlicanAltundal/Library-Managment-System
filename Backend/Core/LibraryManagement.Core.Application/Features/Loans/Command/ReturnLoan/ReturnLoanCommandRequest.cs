using MediatR;

namespace LibraryManagement.Core.Application.Features.Loans.Command.ReturnLoan
{
    public class ReturnLoanCommandRequest : IRequest<Unit>
    {
        public int LoanId { get; set; }
    }
}
