using LibraryManagement.Core.Application.Features.Books.Queries.GetBookById;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQueryRequest, GetAuthorByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAuthorByIdQueryResponse> Handle(GetAuthorByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.GetReadRepository<Author>()
                .GetAsync(
                    x => x.Id == request.Id && !x.IsDeleted,
                    include: q => q
                  .Include(a => a.BookAuthors)
    .ThenInclude(ba => ba.Book),

                    enableTracking: false);

            if (author == null)
                return null;

            return new GetAuthorByIdQueryResponse
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                BookCount = author.BookAuthors?.Count ?? 0
            };
        }
    }
}
