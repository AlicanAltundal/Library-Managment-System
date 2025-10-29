using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Librarians.Queries.GetLoanCountByLibrarian
{
    public class GetLoanCountByLibrarianQueryHandler : IRequestHandler<GetLoanCountByLibrarianQueryRequest, GetLoanCountByLibrarianQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLoanCountByLibrarianQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetLoanCountByLibrarianQueryResponse> Handle(GetLoanCountByLibrarianQueryRequest request, CancellationToken cancellationToken)
        {
            // 🧩 Kütüphaneci bilgisi getir
            var librarian = await _unitOfWork.GetReadRepository<Librarian>()
                .GetAsync(x => x.Id == request.Id && !x.IsDeleted,
                    include: q => q.Include(l => l.Loans));

            if (librarian == null)
                return new GetLoanCountByLibrarianQueryResponse
                {
                    LibrarianId = request.Id,
                    LibrarianName = "Bulunamadı",
                    LoanCount = 0
                };

            // 🧮 Loan sayısı (sadece iade edilmemişleri de sayabilirsin)
            var loanCount = librarian.Loans?.Count(l => !l.IsDeleted) ?? 0;

            return new GetLoanCountByLibrarianQueryResponse
            {
                LibrarianId = librarian.Id,
                LibrarianName = librarian.FullName,
                LoanCount = loanCount
            };
        }
    }
}
