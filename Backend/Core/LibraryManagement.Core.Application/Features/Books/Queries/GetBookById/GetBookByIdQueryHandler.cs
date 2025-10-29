using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQueryRequest, GetBookByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBookByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetBookByIdQueryResponse> Handle(GetBookByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.GetReadRepository<Book>()
                .GetAsync(
                    x => x.Id == request.Id && !x.IsDeleted,
                    include: q => q
                        .Include(b => b.Publisher)
                        .Include(b => b.BookAuthors)
                            .ThenInclude(ba => ba.Author),
                    enableTracking: false);

            if (book == null)
                return null;

            return new GetBookByIdQueryResponse
            {
                ISBN = book.ISBN,
                Title = book.Title,
                Genre = book.Genre,
                PublicationDate = book.PublicationDate,
                PublisherName = book.Publisher?.Name,
                Authors = book.BookAuthors
                    .Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}")
                    .ToList()
            };
        }
    }
}
