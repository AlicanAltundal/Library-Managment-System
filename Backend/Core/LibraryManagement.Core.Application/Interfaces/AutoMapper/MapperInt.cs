using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Interfaces.AutoMapper
{
    public interface MapperInt
    {
        TDestination Map<TDestination, TSource>(TSource source, string? ignore = null);
        IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null);

        TDestination Map<TDestination>(object source, string? ignore = null);

        IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null);
    }
}
