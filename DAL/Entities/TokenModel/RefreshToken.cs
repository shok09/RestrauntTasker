using DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities.TokenModel
{
    public class RefreshToken : BaseEntity<int>
    {
        public RefreshToken(string token, DateTime expires, string userId)
        {
            Token = token;
            Expires = expires;
            UserId = userId;
        }

        public string Token { get; private set; }
        public DateTime Expires { get; private set; }
        public string UserId { get; private set; }
        public bool Active => DateTime.Now <= Expires;
    }
}
