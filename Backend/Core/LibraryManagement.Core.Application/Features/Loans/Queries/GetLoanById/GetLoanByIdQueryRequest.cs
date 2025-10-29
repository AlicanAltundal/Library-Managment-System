using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Loans.Queries.GetLoanById
{
    public class GetLoanByIdQueryRequest : IRequest<GetLoanByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
