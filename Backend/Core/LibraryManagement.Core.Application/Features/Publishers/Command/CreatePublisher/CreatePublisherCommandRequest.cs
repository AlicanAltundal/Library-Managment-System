using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Command.CreatePublisher
{
    public class CreatePublisherCommandRequest : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
