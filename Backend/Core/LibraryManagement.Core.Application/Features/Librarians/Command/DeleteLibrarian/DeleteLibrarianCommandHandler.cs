using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Features.Books.Command.DeleteBook;
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

namespace LibraryManagement.Core.Application.Features.Librarians.Command.DeleteLibrarian
{
    public class DeleteLibrarianCommandHandler : BaseHandler, IRequestHandler<DeleteLibrarianCommandRequest, Unit>
    {
        public DeleteLibrarianCommandHandler(
            MapperInt mapperInt,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteLibrarianCommandRequest request, CancellationToken cancellationToken)
        {
            var librarian = await unitOfWork.GetReadRepository<Librarian>()
                .GetAsync(x => x.Id == request.Id);

            if (librarian == null)
                throw new Exception("Silinmek istenen görevli bulunamadı.");

            // 2️⃣ Soft delete
            librarian.IsDeleted = true;

            // 3️⃣ Güncelle ve kaydet
            await unitOfWork.GetWriteRepository<Librarian>().UpdateAsync(librarian);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
