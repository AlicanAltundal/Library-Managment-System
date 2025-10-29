using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Publishers.Command.UpdatePublisher
{
    public class UpdatePublisherCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
    }

}
