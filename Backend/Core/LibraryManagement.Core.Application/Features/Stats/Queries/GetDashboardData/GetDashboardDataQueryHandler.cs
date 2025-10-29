using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Stats.Queries.GetDashboardData
{
    public class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQueryRequest, GetDashboardDataQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDashboardDataQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetDashboardDataQueryResponse> Handle(GetDashboardDataQueryRequest request, CancellationToken cancellationToken)
        {
            // 🧩 Ana istatistikler
            var totalBooks = await _unitOfWork.GetReadRepository<Book>().CountAsync(b => !b.IsDeleted);
            var totalMembers = await _unitOfWork.GetReadRepository<Member>().CountAsync(m => !m.IsDeleted);
            var totalLoans = await _unitOfWork.GetReadRepository<Loan>().CountAsync(l => !l.IsDeleted);
            var totalPublishers = await _unitOfWork.GetReadRepository<Publisher>().CountAsync(m => !m.IsDeleted);

            // ⏳ Geciken ödünçler
            var overdueCount = await _unitOfWork.GetReadRepository<Loan>()
                .CountAsync(l => !l.IsDeleted && l.ReturnDate == null && l.DueDate < DateTime.Now);

            // 🔄 Aktif & İade edilmiş ödünçler
            var activeLoans = await _unitOfWork.GetReadRepository<Loan>()
                .CountAsync(l => !l.IsDeleted && l.ReturnDate == null);

            var returnedLoans = await _unitOfWork.GetReadRepository<Loan>()
                .CountAsync(l => !l.IsDeleted && l.ReturnDate != null);

            // 🧾 Response modeli
            return new GetDashboardDataQueryResponse
            {
                TotalBooks = totalBooks,
                TotalMembers = totalMembers,
                TotalPublishers = totalPublishers,
                TotalLoans = totalLoans,
                OverdueCount = overdueCount,
                ActiveLoans = activeLoans,
                ReturnedLoans = returnedLoans
            };
        }
    }
}
