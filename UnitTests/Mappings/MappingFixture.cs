using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace RestrauntTasker.UnitTests.Mappings
{
    public class MappingFixture
    {
        public MappingFixture() =>
            Mapper = MapperFactory.Create();

        public IMapper Mapper { get; set; }
    }

}
