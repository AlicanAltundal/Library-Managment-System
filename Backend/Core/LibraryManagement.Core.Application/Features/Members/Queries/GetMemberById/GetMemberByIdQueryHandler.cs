using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Members.Queries.GetMemberById
{
    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQueryRequest, GetMemberByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMemberByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetMemberByIdQueryResponse> Handle(GetMemberByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var member = await _unitOfWork.GetReadRepository<Member>()
                .GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (member == null)
                return null;

            return new GetMemberByIdQueryResponse
            {
                Id = member.Id,
                MemberCode = member.MemberCode,
                FullName = member.FullName,
                Address = member.Address,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email
            };
        }
    }
}
