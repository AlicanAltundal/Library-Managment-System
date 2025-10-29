using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Librarians.Command.CreateLibrarian
{
    public class CreateLibrarianCommandRequest : IRequest<Unit>
    {
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
    }
}
