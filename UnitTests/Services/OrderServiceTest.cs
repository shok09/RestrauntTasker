using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using DAL.UnitOfWork;
using DAL.Entities;
using System.Threading.Tasks;
using BLL.DTO;
using RestrauntTasker.UnitTests.TestConfigurations;

namespace RestrauntTasker.UnitTests.Services
{
    public class OrderServiceTest : TestBaseFixture
    {
        readonly IOrderService orderService;
        public OrderServiceTest() : base() =>
            orderService = new OrderService(new UnitOfWork(Context), Mapper);

        [Theory]
        [InlineData(1)]
        public void Oeder_ReturnOrderWithTitle_IsEqual(int id)
        {

            var result = orderService.GetOrderByIdAsync(id);

            var supposedOrder = new Order { OrderChef = new OrderUser { Name = "Anna" } };

            Assert.Equal(supposedOrder.OrderChef.Name, result.Result.Chef);
        }
    }
}