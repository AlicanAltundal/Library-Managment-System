using LibraryManagement.Core.Application.Features.Members.Queries.GetAllMembers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Queries.GetAllPublishers
{
    public class GetAllPublishersQueryRequest : IRequest<IList<GetAllPublishersQueryResponse>>
    {

    }
}
