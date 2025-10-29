using MediatR;

namespace LibraryManagement.Core.Application.Features.Members.Queries.GetMemberById
{
    public class GetMemberByIdQueryRequest : IRequest<GetMemberByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
