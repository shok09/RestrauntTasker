using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddPerformerAsync(OrderDTO projectDTO, UserDTO performer);
        Task ChangeDescriptionAsync(OrderDTO projectDTO, string description);
        Task ChangeManagerAsync(OrderDTO projectDTO, UserDTO manager);
        Task CreateOrderAsync(OrderDTO projectDTO);
        Task DeleteOrderAsync(OrderDTO projectDTO, UserDTO manager);
        Task<IEnumerable<OrderDTO>> GetByManagerAsync(string name);
        Task<OrderDTO> GetOrderByIdAsync(int id);
    }
}
