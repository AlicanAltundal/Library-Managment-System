using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Loans.Command.ReturnLoan
{
    public class ReturnLoanCommandHandler : BaseHandler, IRequestHandler<ReturnLoanCommandRequest, Unit>
    {
        public ReturnLoanCommandHandler(MapperInt mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(ReturnLoanCommandRequest request, CancellationToken cancellationToken)
        {
            // ✅ 1️⃣ Loan var mı?
            var loan = await unitOfWork.GetReadRepository<Loan>()
                .GetAsync(x => x.Id == request.LoanId && !x.IsDeleted);

            if (loan == null)
                throw new Exception("İade edilmek istenen ödünç kaydı bulunamadı.");

            // ✅ 2️⃣ Zaten iade edilmiş mi?
            if (loan.ReturnDate != null)
                throw new Exception("Bu kitap zaten iade edilmiş.");

            // ✅ 3️⃣ İade işlemini güncelle
            loan.ReturnDate = DateTime.UtcNow;

            await unitOfWork.GetWriteRepository<Loan>().UpdateAsync(loan);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
