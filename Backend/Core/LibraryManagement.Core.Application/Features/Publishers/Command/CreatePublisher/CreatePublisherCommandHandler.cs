using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Features.Members.Command.CreateMember;
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

namespace LibraryManagement.Core.Application.Features.Publishers.Command.CreatePublisher
{
    public class CreatePublisherCommandHandler : BaseHandler, IRequestHandler<CreatePublisherCommandRequest, Unit>
    {
        public CreatePublisherCommandHandler(MapperInt mapperInt, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreatePublisherCommandRequest request, CancellationToken cancellationToken)
        {
            var publisher = new Publisher
            {
                Name = request.Name,
                Address = request.Address,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            // 2️⃣ Kitabı ekle ve kaydet -> ID oluşturulsun
            await unitOfWork.GetWriteRepository<Publisher>().AddAsync(publisher);
            await unitOfWork.SaveAsync();


            return Unit.Value;
        }
    }
}
