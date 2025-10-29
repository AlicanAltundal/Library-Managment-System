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

namespace LibraryManagement.Core.Application.Features.Members.Command.UpdateMember
{
    public class UpdateMemberCommandHandler : BaseHandler, IRequestHandler<UpdateMemberCommandRequest, Unit>
    {
        public UpdateMemberCommandHandler(
            MapperInt mapperInt,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(UpdateMemberCommandRequest request, CancellationToken cancellationToken)
        {
            var member = await unitOfWork.GetReadRepository<Member>()
      .GetAsync(
          predicate: x => x.Id == request.Id && !x.IsDeleted
      );

            if (member == null)
                throw new Exception("Kitap bulunamadı.");

            // 2️⃣ Alanları güncelle
            member.MemberCode = request.MemberCode;
            member.FullName = request.FullName;
            member.Address = request.Address;
            member.PhoneNumber = request.PhoneNumber;
            member.Email = request.Email;

            // 3️⃣ Güncelle ve kaydet
            await unitOfWork.GetWriteRepository<Member>().UpdateAsync(member);
            await unitOfWork.SaveAsync();

            return Unit.Value;

        }
    }
}
