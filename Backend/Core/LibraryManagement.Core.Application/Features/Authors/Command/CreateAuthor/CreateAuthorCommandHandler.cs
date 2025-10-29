using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Authors.Command.CreateAuthor
{
    public class CreateAuthorCommandHandler : BaseHandler, IRequestHandler<CreateAuthorCommandRequest, Unit>
    {
        public CreateAuthorCommandHandler(MapperInt mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateAuthorCommandRequest request, CancellationToken cancellationToken)
        {
            // Boş isim kontrolü (opsiyonel)
            if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
                throw new Exception("Yazar adı ve soyadı boş olamaz.");

            // Aynı isimde yazar zaten var mı?
            var existingAuthor = await unitOfWork.GetReadRepository<Author>()
                .GetAsync(x => x.FirstName == request.FirstName && x.LastName == request.LastName && !x.IsDeleted);

            if (existingAuthor != null)
                throw new Exception("Bu yazar zaten sistemde kayıtlı.");

            var author = new Author
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            await unitOfWork.GetWriteRepository<Author>().AddAsync(author);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
