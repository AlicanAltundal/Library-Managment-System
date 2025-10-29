using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.Core.Application.Features.Search.Queries
{
    public class SearchQueryRequest : IRequest<SearchQueryResponse>
    {
        public string Keyword { get; set; }
    }
}
