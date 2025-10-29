using MediatR;
using System;

namespace LibraryManagement.Core.Application.Features.Loans.Command.CreateLoan
{
    public class CreateLoanCommandRequest : IRequest<Unit>
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int LibrarianId { get; set; }
        public DateTime DueDate { get; set; }   // Son teslim tarihi

    }
}
