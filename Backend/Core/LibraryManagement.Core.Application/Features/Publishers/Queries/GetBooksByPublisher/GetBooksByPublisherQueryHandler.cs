using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Queries.GetBooksByPublisher
{
    public class GetBooksByPublisherQueryHandler : IRequestHandler<GetBooksByPublisherQueryRequest, IList<GetBooksByPublisherQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBooksByPublisherQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetBooksByPublisherQueryResponse>> Handle(GetBooksByPublisherQueryRequest request, CancellationToken cancellationToken)
        {
            var publisher = await _unitOfWork.GetReadRepository<Publisher>()
                .GetAsync(
                    x => x.Id == request.Id && !x.IsDeleted,
                    include: q => q
                        .Include(p => p.Books)
                            .ThenInclude(b => b.BookAuthors)
                                .ThenInclude(ba => ba.Author),
                    enableTracking: false);

            if (publisher == null)
                return new List<GetBooksByPublisherQueryResponse>();

            var response = publisher.Books
                .Where(b => !b.IsDeleted)
                .Select(b => new GetBooksByPublisherQueryResponse
                {
                    BookId = b.Id,
                    ISBN = b.ISBN,
                    Title = b.Title,
                    Genre = b.Genre,
                    PublicationDate = b.PublicationDate,
                    Authors = b.BookAuthors
                        .Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}")
                        .ToList()
                })
                .ToList();

            return response;
        }
    }
}
