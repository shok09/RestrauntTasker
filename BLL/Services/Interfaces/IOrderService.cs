using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddPerformerAsync(OrderDTO orderDTO, UserDTO performer);
        Task ChangeDescriptionAsync(OrderDTO orderDTO, string description);
        Task ChangeManagerAsync(OrderDTO orderDTO, UserDTO manager);
        Task CreateOrderAsync(OrderDTO orderDTO);
        Task DeleteOrderAsync(OrderDTO orderDTO, UserDTO manager);
        Task<IEnumerable<OrderDTO>> GetByManagerAsync(string name);
        Task<OrderDTO> GetOrderByIdAsync(int id);
    }
}
