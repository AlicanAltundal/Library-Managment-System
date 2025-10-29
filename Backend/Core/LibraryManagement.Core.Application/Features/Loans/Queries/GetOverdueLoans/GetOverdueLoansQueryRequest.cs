using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.Core.Application.Features.Loans.Queries.GetOverdueLoans
{
    public class GetOverdueLoansQueryRequest : IRequest<IList<GetOverdueLoansQueryResponse>>
    {
    }
}
