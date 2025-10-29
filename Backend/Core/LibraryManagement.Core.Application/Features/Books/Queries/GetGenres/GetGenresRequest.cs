using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.Core.Application.Features.Books.Queries.GetGenres
{
    public class GetGenresQueryRequest : IRequest<IList<GetGenresQueryResponse>>
    {
    }
}
