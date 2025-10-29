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

namespace LibraryManagement.Core.Application.Features.Members.Queries.GetAllMembers
{
    public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQueryRequest, IList<GetAllMembersQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MapperInt _mapper;

        public GetAllMembersQueryHandler(IUnitOfWork unitOfWork, MapperInt mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetAllMembersQueryResponse>> Handle(GetAllMembersQueryRequest request, CancellationToken cancellationToken)
        {
            var members = await _unitOfWork.GetReadRepository<Member>()
        .GetAllAsync(predicate: m => !m.IsDeleted, enableTracking: false);
            ;

            var response = members.Select(e => new GetAllMembersQueryResponse
            {
                Id = e.Id,
                MemberCode = e.MemberCode,
                FullName = e.FullName,
                Email = e.Email,
                Address = e.Address,
                PhoneNumber = e.PhoneNumber,
                Role = e.Role
            }).ToList();

            return response;
        }
    }
}
