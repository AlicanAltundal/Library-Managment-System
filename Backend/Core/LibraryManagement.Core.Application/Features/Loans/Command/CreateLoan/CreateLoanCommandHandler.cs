using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Loans.Command.CreateLoan
{
    public class CreateLoanCommandHandler : BaseHandler, IRequestHandler<CreateLoanCommandRequest, Unit>
    {
        public CreateLoanCommandHandler(MapperInt mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateLoanCommandRequest request, CancellationToken cancellationToken)
        {
            var activeLoan = await unitOfWork.GetReadRepository<Loan>()
                .GetAsync(x => x.BookId == request.BookId && x.ReturnDate == null);

            if (activeLoan != null)
                throw new Exception("Bu kitap şu anda başka bir üyede ödünç olarak bulunuyor.");

            var bookExists = await unitOfWork.GetReadRepository<Book>()
                .GetAsync(x => x.Id == request.BookId && !x.IsDeleted);

            if (bookExists == null)
                throw new Exception("Kitap bulunamadı.");

            var memberExists = await unitOfWork.GetReadRepository<Member>()
                .GetAsync(x => x.Id == request.MemberId && !x.IsDeleted);

            if (memberExists == null)
                throw new Exception("Üye bulunamadı.");

            var librarianExists = await unitOfWork.GetReadRepository<Librarian>()
                .GetAsync(x => x.Id == request.LibrarianId && !x.IsDeleted);

            if (librarianExists == null)
                throw new Exception("Kütüphaneci bulunamadı.");

            var loan = new Loan
            {
                BookId = request.BookId,
                MemberId = request.MemberId,
                LibrarianId = request.LibrarianId,
                LoanDate = DateTime.UtcNow,
                DueDate = request.DueDate,
                ReturnDate = null 
            };

            await unitOfWork.GetWriteRepository<Loan>().AddAsync(loan);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
