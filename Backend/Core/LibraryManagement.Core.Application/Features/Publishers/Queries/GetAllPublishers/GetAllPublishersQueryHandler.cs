using LibraryManagement.Core.Application.Features.Members.Queries.GetAllMembers;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Queries.GetAllPublishers
{
    public class GetAllPublishersQueryHandler : IRequestHandler<GetAllPublishersQueryRequest, IList<GetAllPublishersQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MapperInt _mapper;

        public GetAllPublishersQueryHandler(IUnitOfWork unitOfWork, MapperInt mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetAllPublishersQueryResponse>> Handle(GetAllPublishersQueryRequest request, CancellationToken cancellationToken)
        {
            var members = await _unitOfWork.GetReadRepository<Publisher>()
        .GetAllAsync(predicate: m => !m.IsDeleted,
             include: query => query
            .Include(x => x.Books),
          enableTracking: false);
            ;

            var response = members.Select(e => new GetAllPublishersQueryResponse
            {
                Id = e.Id,
                Name = e.Name,
                Address = e.Address,
                  BookCount = e.Books?.Count ?? 0 // 🔥 kitap sayısını hesapla
            }).ToList();

            return response;
        }
    }
}
