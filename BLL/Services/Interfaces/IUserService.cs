using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserDTO userDTO);
        Task<UserDTO> GetUserByNameAsync(string name);
        Task AddRefreshTokenAsync(string refreshToken, string IdentityId);
        Task ExchangeRefreshTokenAsync(string oldToken, string newToken, string IdentityId);
    }
}
