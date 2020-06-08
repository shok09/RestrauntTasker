using BLL.DTO;
using BLL.Services;
using DAL.Entities;
using DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(IOrderService orderService) =>
            _orderService = orderService;

        readonly IOrderService _orderService;

        [HttpGet]
        [Route("order/{id}")]
        [Authorize(Policy = "RequirePerformerRole")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.
                GetOrderByIdAsync(id);

            return Ok(order);
        }

        [HttpPost]
        [Route("order/add")]
        public async Task<IActionResult> AddOrderAsync([FromBody] OrderDTO orderDTO)
        {
            await _orderService.CreateOrderAsync(orderDTO);

            return Ok();
        }
    }
}
