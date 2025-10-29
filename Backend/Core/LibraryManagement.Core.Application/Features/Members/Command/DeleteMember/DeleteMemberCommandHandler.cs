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

namespace LibraryManagement.Core.Application.Features.Members.Command.DeleteMember
{
    public class DeleteMemberCommandHandler : BaseHandler, IRequestHandler<DeleteMemberCommandRequest, Unit>
    {
        public DeleteMemberCommandHandler(
            MapperInt mapperInt,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteMemberCommandRequest request, CancellationToken cancellationToken)
        {
            var member = await unitOfWork.GetReadRepository<Member>()
             .GetAsync(x => x.Id == request.Id);

            if (member == null)
                throw new Exception("Silinmek istenen üye bulunamadı.");

            // 2️⃣ Soft delete
            member.IsDeleted = true;

            // 3️⃣ Güncelle ve kaydet
            await unitOfWork.GetWriteRepository<Member>().UpdateAsync(member);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
