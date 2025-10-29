using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Features.Members.Command.UpdateMember;
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

namespace LibraryManagement.Core.Application.Features.Publishers.Command.UpdatePublisher
{
    public class UpdatePublisherCommandHandler : BaseHandler, IRequestHandler<UpdatePublisherCommandRequest, Unit>
    {
        public UpdatePublisherCommandHandler(
            MapperInt mapperInt,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(UpdatePublisherCommandRequest request, CancellationToken cancellationToken)
        {
            var publisher = await unitOfWork.GetReadRepository<Publisher>()
      .GetAsync(
          predicate: x => x.Id == request.Id && !x.IsDeleted
      );

            if (publisher == null)
            {
                throw new Exception("Yayıncı bulunamadı.");
            }

            publisher.Name = request.Name;
            publisher.Address = request.Address;


            // 3️⃣ Güncelle ve kaydet
            await unitOfWork.GetWriteRepository<Publisher>().UpdateAsync(publisher);
            await unitOfWork.SaveAsync();

            return Unit.Value;

        }
    }
}

