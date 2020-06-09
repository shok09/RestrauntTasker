using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RestrauntTasker.UnitTests.Mappings
{
    public class MappingTest : IClassFixture<MappingFixture>
    { 
        private readonly IMapper mapper;

        public MappingTest(MappingFixture fixture) =>
            mapper = fixture.Mapper;

        [Fact]
        public void IMapper_ValidConfig_DesiredConfig()
        {
            mapper.ConfigurationProvider
                .AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Order), typeof(OrderDTO))]
        [InlineData(typeof(OrderUser), typeof(UserDTO))]
        public void MappingFromSourceToDestination_TestPassed(Type source, Type destination)
        {
            var example = Activator.CreateInstance(source);

            mapper.Map(example, source, destination);
        }
    }
}