using LibraryManagement.Core.Application.Features.Members.Queries.GetMemberById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Queries.GetPublisherById
{
    public class GetPublisherByIdQueryRequest : IRequest<GetPublisherByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
