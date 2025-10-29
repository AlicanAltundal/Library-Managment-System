using AutoMapper;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.CustomMapper.CustomAutoMapper
{
    public record TypePair(Type SourceType, Type DestinationType);

    public class Mapper : MapperInt
    {
        public static List<TypePair> typePairs = new();
        private IMapper MapperContainer;

        public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
        {
            Config<TDestination, TSource>(5, ignore);
            return MapperContainer.Map<TSource, TDestination>(source);
        }

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null)
        {
            Config<TDestination, TSource>(5, ignore);
            return MapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
        }

        public TDestination Map<TDestination>(object source, string? ignore = null)
        {
            Config<TDestination, object>(5, ignore);
            return MapperContainer.Map<TDestination>(source);
        }

        public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
        {
            Config<TDestination, IList<object>>(5, ignore);
            return MapperContainer.Map<IList<TDestination>>(source);
        }

        protected void Config<TDestination, TSource>(int depth = 5, string? ignore = null)
        {
            var typePair = new TypePair(typeof(TSource), typeof(TDestination));

            if (typePairs.Any(a => a.DestinationType == typePair.DestinationType &&
                                   a.SourceType == typePair.SourceType) && ignore is null)
                return;

            typePairs.Add(typePair);

            // <-- Burada sadece global:: ekledik
            var config = new global::AutoMapper.MapperConfiguration(cfg =>
            {
                foreach (var pair in typePairs)
                {
                    if (ignore is null)
                    {
                        cfg.CreateMap(pair.SourceType, pair.DestinationType)
                           .MaxDepth(depth)
                           .ReverseMap();
                    }
                    else
                    {
                        cfg.CreateMap(pair.SourceType, pair.DestinationType)
                           .MaxDepth(depth)
                           .ForMember(ignore, x => x.Ignore())
                           .ReverseMap();
                    }
                }
            });

            MapperContainer = config.CreateMapper();
        }
    }
}

