using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.Core.Application.Features.Members.Queries.GetLoansByMember
{
    public class GetLoansByMemberQueryRequest : IRequest<IList<GetLoansByMemberQueryResponse>>
    {
        public int MemberId { get; set; }
    }
}
