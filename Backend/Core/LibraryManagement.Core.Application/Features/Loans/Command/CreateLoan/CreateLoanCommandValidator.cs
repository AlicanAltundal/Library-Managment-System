using FluentValidation;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Loans.Command.CreateLoan
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommandRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLoanCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Geçerli bir kitap seçiniz.")
                .MustAsync(BookMustExist).WithMessage("Bu kitap sistemde kayıtlı değil.");

            RuleFor(x => x.MemberId)
                .GreaterThan(0).WithMessage("Geçerli bir üye seçiniz.")
                .MustAsync(MemberMustExist).WithMessage("Bu üye sistemde kayıtlı değil.");

            RuleFor(x => x.LibrarianId)
                .GreaterThan(0).WithMessage("Geçerli bir kütüphaneci seçiniz.")
                .MustAsync(LibrarianMustExist).WithMessage("Bu kütüphaneci sistemde kayıtlı değil.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Son teslim tarihi bugünden ileri olmalıdır.");
        }

        private async Task<bool> BookMustExist(int bookId, CancellationToken token)
        {
            var book = await _unitOfWork.GetReadRepository<Book>()
                .GetAsync(x => x.Id == bookId && !x.IsDeleted);
            return book != null;
        }

        private async Task<bool> MemberMustExist(int memberId, CancellationToken token)
        {
            var member = await _unitOfWork.GetReadRepository<Member>()
                .GetAsync(x => x.Id == memberId && !x.IsDeleted);
            return member != null;
        }

        private async Task<bool> LibrarianMustExist(int librarianId, CancellationToken token)
        {
            var librarian = await _unitOfWork.GetReadRepository<Librarian>()
                .GetAsync(x => x.Id == librarianId && !x.IsDeleted);
            return librarian != null;
        }
    }
}
