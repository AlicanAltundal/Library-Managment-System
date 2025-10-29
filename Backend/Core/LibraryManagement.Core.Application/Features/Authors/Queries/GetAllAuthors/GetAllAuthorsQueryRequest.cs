using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.Core.Application.Features.Authors.Queries.GetAllAuthors
{
    public class GetAllAuthorsQueryRequest : IRequest<IList<GetAllAuthorsQueryResponse>>
    {
    }
}
