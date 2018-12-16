using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WMIP.Automapper
{
    public class MapperConfig
    {
        public MapperConfiguration Execute(Assembly assembly)
        {
            return new MapperConfiguration(
                cfg =>
                {
                    IEnumerable<Type> types = assembly.GetExportedTypes();
                    LoadBothWaysMapping(types, cfg);
                    LoadStandardMappings(types, cfg);
                    LoadReverseMappings(types, cfg);
                    LoadCustomMappings(types, cfg);
                });
        }

        private void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfig)
        {
            IHaveCustomMappings[] maps = (from t in types
                                          from i in t.GetInterfaces()
                                          where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                                                !t.IsAbstract &&
                                                !t.IsInterface
                                          select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (IHaveCustomMappings map in maps)
            {
                map.CreateMappings(mapperConfig);
            }
        }

        private void LoadReverseMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfig)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Destination = i.GetGenericArguments()[0],
                            Source = t,
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfig.CreateMap(map.Source, map.Destination);
            }
        }

        private void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfig)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t,
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfig.CreateMap(map.Source, map.Destination);
            }
        }

        private void LoadBothWaysMapping(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfig)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapBothWays<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t,
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfig.CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }
    }
}
