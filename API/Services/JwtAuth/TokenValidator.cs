using API.Services.JwtAuth.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.JwtAuth
{
    public class TokenValidator : ITokenValidator
    {
        readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler; 
        public TokenValidator() =>
            _jwtSecurityTokenHandler ??= new JwtSecurityTokenHandler();
        public ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                ValidateLifetime = false
            };

            var principal = _jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out var securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new Exception(""); //Create specific exception for this case

            return principal;
        }
    }
}
