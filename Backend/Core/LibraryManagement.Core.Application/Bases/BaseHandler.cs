using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Bases
{
    public class BaseHandler
    {
        public readonly MapperInt mapperInt;
        public readonly IUnitOfWork unitOfWork;
        public readonly IHttpContextAccessor httpContextAccessor;
        public readonly string userId;

        public BaseHandler(MapperInt mapperInt, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {

            this.mapperInt = mapperInt;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor=httpContextAccessor;
            userId = httpContextAccessor.HttpContext?.User?
    .Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        }
    }
}
