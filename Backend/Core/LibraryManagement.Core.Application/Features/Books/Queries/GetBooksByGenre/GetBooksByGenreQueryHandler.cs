using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Queries.GetBooksByGenre
{
    public class GetBooksByGenreQueryHandler : IRequestHandler<GetBooksByGenreQueryRequest, IList<GetBooksByGenreQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBooksByGenreQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetBooksByGenreQueryResponse>> Handle(GetBooksByGenreQueryRequest request, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.GetReadRepository<Book>()
                .GetAllAsync(
                    b => b.Genre.ToLower() == request.Genre.ToLower() && !b.IsDeleted,
                    include: q => q.Include(x => x.Publisher),
                    enableTracking: false
                );

            return books.Select(b => new GetBooksByGenreQueryResponse
            {
                ISBN = b.ISBN,
                Title = b.Title,
                Genre = b.Genre,
                PublicationDate = b.PublicationDate,
                PublisherName = b.Publisher?.Name
            }).ToList();
        }
    }
}
