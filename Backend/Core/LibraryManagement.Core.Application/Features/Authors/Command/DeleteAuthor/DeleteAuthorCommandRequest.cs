using MediatR;

namespace LibraryManagement.Core.Application.Features.Authors.Command.DeleteAuthor
{
    public class DeleteAuthorCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
