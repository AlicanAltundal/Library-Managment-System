using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Authors.Command.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : BaseHandler, IRequestHandler<DeleteAuthorCommandRequest, Unit>
    {
        public DeleteAuthorCommandHandler(
            MapperInt mapperInt,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteAuthorCommandRequest request, CancellationToken cancellationToken)
        {
            // 1️⃣ Yazar var mı?
            var author = await unitOfWork.GetReadRepository<Author>()
                .GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (author == null)
                throw new Exception("Yazar bulunamadı.");

            // 2️⃣ Soft delete işlemi
            author.IsDeleted = true;

            await unitOfWork.GetWriteRepository<Author>().UpdateAsync(author);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
