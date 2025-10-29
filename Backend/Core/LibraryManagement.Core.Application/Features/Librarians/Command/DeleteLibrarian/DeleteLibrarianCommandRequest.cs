using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Librarians.Command.DeleteLibrarian
{
    public class DeleteLibrarianCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
    
}
