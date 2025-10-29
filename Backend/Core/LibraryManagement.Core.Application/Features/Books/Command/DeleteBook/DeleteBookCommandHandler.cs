using LibraryManagement.Core.Application.Bases;
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

namespace LibraryManagement.Core.Application.Features.Books.Command.DeleteBook
{
    public class DeleteBookCommandHandler : BaseHandler, IRequestHandler<DeleteBookCommandRequest, Unit>
    {
        public DeleteBookCommandHandler(
            MapperInt mapperInt,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteBookCommandRequest request, CancellationToken cancellationToken)
        {
            // 1️⃣ Oteli bul (aktif veya pasif olabilir)
            var book = await unitOfWork.GetReadRepository<Book>()
                .GetAsync(x => x.Id == request.Id);

            if (book == null)
                throw new Exception("Silinmek istenen kitap bulunamadı.");

            // 2️⃣ Soft delete
            book.IsDeleted = true;

            // 3️⃣ Güncelle ve kaydet
            await unitOfWork.GetWriteRepository<Book>().UpdateAsync(book);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
