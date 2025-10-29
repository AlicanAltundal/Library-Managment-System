using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Loans.Command.ReturnLoan
{
    public class ReturnLoanCommandValidator : AbstractValidator<ReturnLoanCommandRequest>
    {
        public ReturnLoanCommandValidator()
        {
            RuleFor(x => x.LoanId)
    .GreaterThan(0).WithMessage("Geçerli bir ödünç Id seçiniz.");
        }
    }
}
