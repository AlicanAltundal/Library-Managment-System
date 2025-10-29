using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Features.Books.Command.UpdateBook;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Authors.Command.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : BaseHandler, IRequestHandler<UpdateAuthorCommandRequest, Unit>
    {
        public UpdateAuthorCommandHandler(
            MapperInt mapperInt,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(UpdateAuthorCommandRequest request, CancellationToken cancellationToken)
        {
            var author = await unitOfWork.GetReadRepository<Author>()
    .GetAsync(
        predicate: x => x.Id == request.Id && !x.IsDeleted
    );
            if (author == null)
                throw new Exception("Yazar bulunamadı.");

            // 2️⃣ Alanları güncelle
            author.FirstName = request.FirstName;
            author.LastName = request.LastName;

            // 3️⃣ Güncelle ve kaydet
            await unitOfWork.GetWriteRepository<Author>().UpdateAsync(author);
            await unitOfWork.SaveAsync();

            return Unit.Value;

        }
    }
}
