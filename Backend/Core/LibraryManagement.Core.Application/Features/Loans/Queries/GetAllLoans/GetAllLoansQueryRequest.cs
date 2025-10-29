using LibraryManagement.Core.Application.Features.Books.Queries.GetAllBooks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Loans.Queries.GetAllLoans
{
    public class GetAllLoansQueryRequest : IRequest<IList<GetAllLoansQueryResponse>>
    {
    }
}
