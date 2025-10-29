using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Loans.Queries.GetLoanById
{
    public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQueryRequest, GetLoanByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLoanByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetLoanByIdQueryResponse> Handle(GetLoanByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var loan = await _unitOfWork.GetReadRepository<Loan>()
                .GetAsync(
                    predicate: l => l.Id == request.Id && !l.IsDeleted && l.ReturnDate == null,
                    include: q => q
                        .Include(l => l.Book)
                        .Include(l => l.Member)
                        .Include(l => l.Librarian),
                    enableTracking: false
                );

            if (loan == null)
                return null;

            // 🔹 Response’a map et
            return new GetLoanByIdQueryResponse
            {
                BookId = loan.BookId,
                BookTitle = loan.Book?.Title,
                ISBN = loan.Book?.ISBN,

                MemberId = loan.MemberId,
                MemberName = loan.Member?.FullName,
                MemberCode = loan.Member?.MemberCode,

                LibrarianId = loan.LibrarianId,
                LibrarianName = loan.Librarian?.FullName,

                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate
            };
        }
    }
}
