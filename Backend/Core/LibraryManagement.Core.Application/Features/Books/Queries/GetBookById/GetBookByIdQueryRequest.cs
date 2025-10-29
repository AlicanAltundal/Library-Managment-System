using MediatR;

namespace LibraryManagement.Core.Application.Features.Books.Queries.GetBookById
{
    public class GetBookByIdQueryRequest : IRequest<GetBookByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
