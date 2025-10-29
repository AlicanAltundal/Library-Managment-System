using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Queries.GetAllBooks
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQueryRequest, IList<GetAllBooksQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MapperInt _mapper;

        public GetAllBooksQueryHandler(IUnitOfWork unitOfWork, MapperInt mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetAllBooksQueryResponse>> Handle(GetAllBooksQueryRequest request, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.GetReadRepository<Book>()
    .GetAllAsync(
        predicate: x => !x.IsDeleted,
        include: query => query
            .Include(x => x.Publisher)
            .Include(x => x.BookAuthors)
                .ThenInclude(ba => ba.Author),
        enableTracking: false
    );


            return books.Select(b => new GetAllBooksQueryResponse
            {
                Id = b.Id,
                ISBN = b.ISBN,
                Title = b.Title,
                Genre = b.Genre,
                PublicationDate = b.PublicationDate,
                PublisherName = b.Publisher?.Name,
                PublisherId = b.PublisherId,
                Authors = b.BookAuthors
        .Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}")
        .ToList()
            }).ToList();
        }

    }
}
