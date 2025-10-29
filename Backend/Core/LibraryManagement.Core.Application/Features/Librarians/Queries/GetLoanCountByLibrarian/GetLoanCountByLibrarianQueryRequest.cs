using MediatR;

namespace LibraryManagement.Core.Application.Features.Librarians.Queries.GetLoanCountByLibrarian
{
    public class GetLoanCountByLibrarianQueryRequest : IRequest<GetLoanCountByLibrarianQueryResponse>
    {
        public int Id { get; set; }  // LibrarianId
    }
}
