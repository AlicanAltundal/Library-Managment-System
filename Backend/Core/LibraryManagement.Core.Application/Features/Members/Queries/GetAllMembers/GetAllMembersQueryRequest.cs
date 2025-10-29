using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Members.Queries.GetAllMembers
{
    public class GetAllMembersQueryRequest : IRequest<IList<GetAllMembersQueryResponse>>
    {
    }
}
