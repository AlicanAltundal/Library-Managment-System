using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Queries.CheckIsbn
{
    public class CheckIsbnQueryHandler : IRequestHandler<CheckISBNQueryRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckIsbnQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CheckISBNQueryRequest request, CancellationToken cancellationToken)
        {
            var isbn = request.ISBN?.Trim().ToLower();

            var books = await _unitOfWork.GetReadRepository<Book>()
                .GetAllAsync(b => !b.IsDeleted, enableTracking: false);

            bool exists = books.Any(b => b.ISBN.Trim().ToLower() == isbn);

            return !exists; // yoksa true (kullanılabilir)
        }

    }
}
