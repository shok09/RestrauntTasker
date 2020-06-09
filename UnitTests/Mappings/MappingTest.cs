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
        [InlineData(typeof(Order), typeof(OrderDTO), "Mario")]
        public void MappingFromSourceToDestination_TestPassed(Type source, Type destination, string title)
        {
            var theorder = new Order { Title = title };
            OrderDTO theorderDTO = mapper.Map<OrderDTO>(theorder);
            Assert.Equal(theorderDTO.Title, theorder.Title);
            Assert.Equal(theorder.GetType(), source);
            Assert.Equal(theorderDTO.GetType(), destination);
        }
    }
}