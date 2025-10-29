using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.Core.Application.Features.Publishers.Queries.GetBooksByPublisher
{
    public class GetBooksByPublisherQueryRequest : IRequest<IList<GetBooksByPublisherQueryResponse>>
    {
        public int Id { get; set; }  
    }
}
