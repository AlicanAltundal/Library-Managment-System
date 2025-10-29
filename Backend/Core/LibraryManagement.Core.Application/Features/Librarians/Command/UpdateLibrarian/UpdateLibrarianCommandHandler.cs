using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Features.Books.Command.UpdateBook;
using LibraryManagement.Core.Application.Features.Librarians.Command.UpdateLibrarian;
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

namespace LibraryManagement.Core.Application.Features.Librarians.Command.UpdateLibrarian
{
    public class UpdateLibrarianCommandHandler : BaseHandler, IRequestHandler<UpdateLibrarianCommandRequest, Unit>
    {
        public UpdateLibrarianCommandHandler(
            MapperInt mapperInt,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(UpdateLibrarianCommandRequest request, CancellationToken cancellationToken)
        {
            var librarian = await unitOfWork.GetReadRepository<Librarian>()
    .GetAsync(
        predicate: x => x.Id == request.Id && !x.IsDeleted
    );

            if (librarian == null)
                throw new Exception("Kitap bulunamadı.");

            librarian.EmployeeCode = request.EmployeeCode;
            librarian.FullName = request.FullName;
            librarian.Department = request.Department;

            // 3️⃣ Güncelle ve kaydet
            await unitOfWork.GetWriteRepository<Librarian>().UpdateAsync(librarian);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
