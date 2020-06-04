using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IOrderTaskService
    {
        Task AssignPerformerAsync(OrderTaskDTO projectTaskDTO, UserDTO performer);
        Task CreateTaskAsync(OrderTaskDTO projectTaskDTO);
        Task ChangeDesriptionAsync(OrderTaskDTO projectTaskDTO, string description);
    }
}
