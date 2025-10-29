using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Features.Books.Command.CreateBook;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using BC = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Members.Command.CreateMember
{
    public class CreateMemberCommandHandler : BaseHandler, IRequestHandler<CreateMemberCommandRequest, Unit>
    {
        public CreateMemberCommandHandler(MapperInt mapperInt, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateMemberCommandRequest request, CancellationToken cancellationToken)
        {
            var member = new Member
            {
                Role = request.Role,
                MemberCode = request.MemberCode,
                FullName = request.FullName,
                Address = request.Address,
                PasswordHash = BC.HashPassword(request.Password),
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
            };

            // 2️⃣ Kitabı ekle ve kaydet -> ID oluşturulsun
            await unitOfWork.GetWriteRepository<Member>().AddAsync(member);
            await unitOfWork.SaveAsync();


            return Unit.Value;
        }
    }
}
