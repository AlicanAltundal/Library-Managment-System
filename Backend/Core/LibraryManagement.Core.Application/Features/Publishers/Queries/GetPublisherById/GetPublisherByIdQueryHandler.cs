using LibraryManagement.Core.Application.Features.Members.Queries.GetMemberById;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Queries.GetPublisherById
{
    public class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQueryRequest, GetPublisherByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPublisherByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetPublisherByIdQueryResponse> Handle(GetPublisherByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var publisher = await _unitOfWork.GetReadRepository<Publisher>()
               .GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (publisher == null)
                return null;

            return new GetPublisherByIdQueryResponse
            {
                Id = publisher.Id,
                Name = publisher.Name,

                Address = publisher.Address,


            };
        }
    }
}
