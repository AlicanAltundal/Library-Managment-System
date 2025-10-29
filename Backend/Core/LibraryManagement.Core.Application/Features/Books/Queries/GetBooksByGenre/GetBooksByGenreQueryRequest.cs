using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.Core.Application.Features.Books.Queries.GetBooksByGenre
{
    public class GetBooksByGenreQueryRequest : IRequest<IList<GetBooksByGenreQueryResponse>>
    {
        public string Genre { get; set; }
    }
}
