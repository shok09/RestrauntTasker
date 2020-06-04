﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BLL.Mapper
{
    class MapperProfile : Profile
    {
        public MapperProfile() =>
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            List<Type> types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (Type type in types)
            {
                MethodInfo method = type.GetMethod("Mapping") ??
                    type.GetInterface("IMapFrom`1")
                    .GetMethod("Mapping");

                var instance = Activator.CreateInstance(type);

                method?.Invoke(instance, new object[] { this });
            }
        }
    }
}
