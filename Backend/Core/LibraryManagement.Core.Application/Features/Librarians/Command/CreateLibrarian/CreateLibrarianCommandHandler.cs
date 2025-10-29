using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Features.Librarians.Command.CreateLibrarian;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace LibraryManagement.Core.Application.Features.Librarians.Command.CreateLibrarian
{
    public class CreateLibrarianCommandHandler : BaseHandler, IRequestHandler<CreateLibrarianCommandRequest, Unit>
    {
        public CreateLibrarianCommandHandler(MapperInt mapperInt, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateLibrarianCommandRequest request, CancellationToken cancellationToken)
        {
            var librarian = new LibraryManagement.Core.Domain.Entities.Librarian
            {
                EmployeeCode = request.EmployeeCode,
                FullName = request.FullName,
                Department = request.Department,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
            };

            // 2️⃣ Kitabı ekle ve kaydet -> ID oluşturulsun
            await unitOfWork.GetWriteRepository<LibraryManagement.Core.Domain.Entities.Librarian>().AddAsync(librarian);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
