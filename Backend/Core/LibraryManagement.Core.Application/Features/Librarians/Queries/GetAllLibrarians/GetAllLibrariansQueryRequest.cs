using LibraryManagement.Core.Application.Features.Books.Queries.GetAllBooks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Librarians.Queries.GetAllLibrarians
{
    public class GetAllLibrariansQueryRequest : IRequest<IList<GetAllLibrariansQueryResponse>>
    {
    }
}
