using API.Services.JwtAuth.IssuerOptions;
using API.Services.JwtAuth.TokenModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.JwtAuth
{
    public sealed class JwtFactory : IJwtFactory
    {
        readonly JwtIssuerOptions _jwtOptions;
        
        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions) =>
            _jwtOptions = jwtOptions.Value;

        public async Task<AccessToken> GenerateEncodedToken(string id, string userName, string role)
        {
           
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.GenerateJti()),
                new Claim(JwtRegisteredClaimNames.Iat, _jwtOptions.IssuedAt.ToString(), ClaimValueTypes.Integer64),

                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Role , role)
            };

            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.NotBefore,
                _jwtOptions.Expiration,
                _jwtOptions.SigningCredentials
                );

            return new AccessToken(new JwtSecurityTokenHandler().WriteToken(jwt), (int)_jwtOptions.ValidFor.TotalSeconds);
        }
    }
}
