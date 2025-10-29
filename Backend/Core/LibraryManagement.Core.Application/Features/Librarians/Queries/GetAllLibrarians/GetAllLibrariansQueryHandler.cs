using LibraryManagement.Core.Application.Features.Books.Queries.GetAllBooks;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Librarians.Queries.GetAllLibrarians
{
    public class GetAllLibrariansQueryHandler : IRequestHandler<GetAllLibrariansQueryRequest, IList<GetAllLibrariansQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MapperInt _mapper;

        public GetAllLibrariansQueryHandler(IUnitOfWork unitOfWork, MapperInt mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetAllLibrariansQueryResponse>> Handle(GetAllLibrariansQueryRequest request, CancellationToken cancellationToken)
        {
            var books = await _unitOfWork.GetReadRepository<Librarian>()
.GetAllAsync(
predicate: x => !x.IsDeleted,
enableTracking: false
);

            return books.Select(b => new GetAllLibrariansQueryResponse
            {
                Id = b.Id,
                EmployeeCode = b.EmployeeCode,
                FullName = b.FullName,
                Department = b.Department
            }).ToList();


        }
    }
}
