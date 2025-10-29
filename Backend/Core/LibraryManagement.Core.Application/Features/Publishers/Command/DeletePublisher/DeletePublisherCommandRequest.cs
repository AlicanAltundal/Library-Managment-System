using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Command.DeletePublisher
{
    public class DeletePublisherCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
