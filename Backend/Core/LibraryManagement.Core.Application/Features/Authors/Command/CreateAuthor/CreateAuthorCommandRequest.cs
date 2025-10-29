using MediatR;

namespace LibraryManagement.Core.Application.Features.Authors.Command.CreateAuthor
{
    public class CreateAuthorCommandRequest : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
