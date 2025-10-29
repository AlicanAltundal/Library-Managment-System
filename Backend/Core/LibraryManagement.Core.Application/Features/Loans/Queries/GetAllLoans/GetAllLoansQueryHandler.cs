using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Loans.Queries.GetAllLoans
{
    public class GetAllLoansQueryHandler : IRequestHandler<GetAllLoansQueryRequest, IList<GetAllLoansQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLoansQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetAllLoansQueryResponse>> Handle(GetAllLoansQueryRequest request, CancellationToken cancellationToken)
        {
            var loans = await _unitOfWork.GetReadRepository<Loan>()
                .GetAllAsync(
                        predicate: l => !l.IsDeleted && l.ReturnDate == null,
                    include: q => q
                        .Include(l => l.Book)
                        .Include(l => l.Member)
                        .Include(l => l.Librarian),
                    enableTracking: false
                );

            var response = loans.Select(l => new GetAllLoansQueryResponse
            {
                Id = l.Id,
                BookId = l.BookId,
                BookTitle = l.Book?.Title,
                ISBN = l.Book?.ISBN,

                MemberId = l.MemberId,
                MemberName = l.Member?.FullName,
                MemberCode = l.Member?.MemberCode,

                LibrarianId = l.LibrarianId,
                LibrarianName = l.Librarian?.FullName,

                LoanDate = l.LoanDate,
                DueDate = l.DueDate,
                ReturnDate = l.ReturnDate
            }).ToList();

            return response;
        }
    }
}
