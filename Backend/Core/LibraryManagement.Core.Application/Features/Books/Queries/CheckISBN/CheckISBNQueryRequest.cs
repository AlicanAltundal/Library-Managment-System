using MediatR;

namespace LibraryManagement.Core.Application.Features.Books.Queries.CheckIsbn
{
    public class CheckISBNQueryRequest : IRequest<bool>
    {
        public string ISBN { get; set; }
    }
}
