using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services.JwtAuth.TokenModel
{
    public sealed class AccessToken
    {
        public AccessToken(string token, int expiresIn)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }

        public string Token { get; }
        public int ExpiresIn { get; }
    }
}
