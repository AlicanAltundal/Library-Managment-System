using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.CustomMapper.CustomAutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.CustomMapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<MapperInt, Mapper>();
        }
    }
}
