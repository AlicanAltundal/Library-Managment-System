using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Authors.Queries.GetAllAuthors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQueryRequest, IList<GetAllAuthorsQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetAllAuthorsQueryResponse>> Handle(GetAllAuthorsQueryRequest request, CancellationToken cancellationToken)
        {
            // Yazarları kitap ilişkileriyle birlikte al
            var authors = await _unitOfWork.GetReadRepository<Author>()
                .GetAllAsync(
                    predicate: x => !x.IsDeleted,
                    include: q => q.Include(a => a.BookAuthors),
                    enableTracking: false
                );

            // Response’a map et
            var response = authors.Select(a => new GetAllAuthorsQueryResponse
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                FullName = a.FirstName + " " + a.LastName, // 🔥 Ekledik
                BookCount = a.BookAuthors?.Count ?? 0
            }).ToList();


            return response;
        }
    }
}
