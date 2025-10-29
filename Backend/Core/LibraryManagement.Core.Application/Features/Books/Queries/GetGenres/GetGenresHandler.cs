using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Queries.GetGenres
{
    public class GetGenresQueryHandler : IRequestHandler<GetGenresQueryRequest, IList<GetGenresQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGenresQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetGenresQueryResponse>> Handle(GetGenresQueryRequest request, CancellationToken cancellationToken)
        {
            // 🧩 Tüm kitapları al, silinmemiş olanları grupla
            var genres = await _unitOfWork.GetReadRepository<Book>()
                .Find(b => !b.IsDeleted)
                .GroupBy(b => b.Genre)
                .Select(g => new GetGenresQueryResponse
                {
                    GenreName = g.Key,
                    BookCount = g.Count()
                })
                .OrderBy(g => g.GenreName)
                .ToListAsync(cancellationToken);

            return genres;
        }
    }
}
