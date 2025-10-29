using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Features.Members.Command.DeleteMember;
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

namespace LibraryManagement.Core.Application.Features.Publishers.Command.DeletePublisher
{
    public class DeletePublisherCommandHandler : BaseHandler, IRequestHandler<DeletePublisherCommandRequest, Unit>
    {
        public DeletePublisherCommandHandler(
            MapperInt mapperInt,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeletePublisherCommandRequest request, CancellationToken cancellationToken)
        {
            var publisher = await unitOfWork.GetReadRepository<Publisher>()
  .GetAsync(x => x.Id == request.Id);

            if (publisher == null)
            {
                throw new Exception("Silinmek istenen yayıncı bulunamadı.");
            }

            publisher.IsDeleted = true;

            // 3️⃣ Güncelle ve kaydet
            await unitOfWork.GetWriteRepository<Publisher>().UpdateAsync(publisher);
            await unitOfWork.SaveAsync();

            return Unit.Value;


        }
    }
}
