using API.Services.JwtAuth.TokenModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.JwtAuth
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName, string role);
        string GenerateRefreshToken(int size = 32);
    }
}
