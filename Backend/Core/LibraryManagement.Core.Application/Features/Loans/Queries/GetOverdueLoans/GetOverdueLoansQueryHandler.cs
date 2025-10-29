using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Loans.Queries.GetOverdueLoans
{
    public class GetOverdueLoansQueryHandler : IRequestHandler<GetOverdueLoansQueryRequest, IList<GetOverdueLoansQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOverdueLoansQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetOverdueLoansQueryResponse>> Handle(GetOverdueLoansQueryRequest request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            // 🔹 Geciken = teslim edilmemiş ve vadesi geçmiş kitaplar
            var overdueLoans = await _unitOfWork.GetReadRepository<Loan>()
                .GetAllAsync(
                    predicate: l => !l.IsDeleted && l.ReturnDate == null && l.DueDate < now,
                    include: q => q
                        .Include(l => l.Book)
                        .Include(l => l.Member),
                    enableTracking: false
                );

            var response = overdueLoans.Select(l => new GetOverdueLoansQueryResponse
            {
                Id = l.Id,
                BookId = l.BookId,
                BookTitle = l.Book?.Title,
                ISBN = l.Book?.ISBN,
                MemberId = l.MemberId,
                MemberName = l.Member?.FullName,
                MemberCode = l.Member?.MemberCode,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate
            }).ToList();

            return response;
        }
    }
}
