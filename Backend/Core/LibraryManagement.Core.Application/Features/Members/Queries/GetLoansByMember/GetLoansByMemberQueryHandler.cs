using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Members.Queries.GetLoansByMember
{
    public class GetLoansByMemberQueryHandler : IRequestHandler<GetLoansByMemberQueryRequest, IList<GetLoansByMemberQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLoansByMemberQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetLoansByMemberQueryResponse>> Handle(GetLoansByMemberQueryRequest request, CancellationToken cancellationToken)
        {
            var loans = await _unitOfWork.GetReadRepository<Loan>()
                .GetAllAsync(
                    l => l.MemberId == request.MemberId && !l.IsDeleted,
                    include: q => q.Include(l => l.Book),
                    enableTracking: false
                );

            return loans.Select(l => new GetLoansByMemberQueryResponse
            {
                BookTitle = l.Book?.Title,
                ISBN = l.Book?.ISBN,
                LoanDate = l.LoanDate,
                DueDate = l.DueDate,
                ReturnDate = l.ReturnDate
            }).ToList();
        }
    }
}
