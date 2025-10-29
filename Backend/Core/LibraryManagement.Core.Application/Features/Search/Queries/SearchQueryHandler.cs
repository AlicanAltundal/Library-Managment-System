using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Search.Queries
{
    public class SearchQueryHandler : IRequestHandler<SearchQueryRequest, SearchQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SearchQueryResponse> Handle(SearchQueryRequest request, CancellationToken cancellationToken)
        {
            var keyword = request.Keyword.ToLower();

            var books = await _unitOfWork.GetReadRepository<Book>()
     .Find(b => b.Title.ToLower().Contains(keyword) && !b.IsDeleted)
     .Select(b => new BookSearchDto(b.Id, b.Title))
     .ToListAsync();


            var authors = await _unitOfWork.GetReadRepository<Author>()
                .Find(a => (a.FirstName + " " + a.LastName).ToLower().Contains(keyword) && !a.IsDeleted)
                .Select(a => new AuthorSearchDto(a.Id, a.FirstName, a.LastName))
                .ToListAsync();

            var members = await _unitOfWork.GetReadRepository<Member>()
                .Find(m => m.FullName.ToLower().Contains(keyword) && !m.IsDeleted)
                .Select(m => new MemberSearchDto(m.Id, m.FullName))
                .ToListAsync();

            return new SearchQueryResponse
            {
                Books = books,
                Authors = authors,
                Members = members
            };
        }
    }
}
