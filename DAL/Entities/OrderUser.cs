using DAL.Entities.Abstract;
using DAL.Entities.Enums;
using DAL.Entities.IdentityModel;
using DAL.Entities.TokenModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Entities
{
    public class OrderUser : BaseEntity<int>
    {
        public OrderUser() => Tasks = new HashSet<OrderTask>();

        public string Name { get; set; }
        public string UserContactsId { get; set; }
        public UserRole Role { get; set; }
        public int OrderId { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual UserContacts UserContacts { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<OrderTask> Tasks { get; set; }

        readonly List<RefreshToken> refreshTokens = new List<RefreshToken>();
        public virtual IReadOnlyCollection<RefreshToken> RefreshTokens => refreshTokens.AsReadOnly();

        public bool HasValidRefreshToken(string refreshToken) =>
            refreshTokens.Any(rt => rt.Token == refreshToken && rt.Active);

        public void AddRefreshToken(string token, string userId, double daysToExpire = 5) =>
            refreshTokens.Add(new RefreshToken(token, DateTime.UtcNow.AddDays(daysToExpire), userId));

        public void RemoveRefreshToken(string refreshToken) =>
            refreshTokens.Remove(refreshTokens.First(rt => rt.Token == refreshToken));
    }
}
