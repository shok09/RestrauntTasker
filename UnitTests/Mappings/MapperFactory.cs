using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.Mapper;

namespace RestrauntTasker.UnitTests.Mappings
{
    public static class MapperFactory
    {
        public static IMapper Create()
        {
            var configProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            return configProvider.CreateMapper();
        }
    }
}