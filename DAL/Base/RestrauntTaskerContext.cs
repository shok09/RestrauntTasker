using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DAL.Entities.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DAL.Entities.TokenModel;

namespace DAL.Base
{
    internal class RestrauntTaskerContext : IdentityDbContext<ApplicationUser>
    {
        public RestrauntTaskerContext(DbContextOptions<RestrauntTaskerContext> options)
            : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderTask> Tasks { get; set; }
        public DbSet<OrderUser> OrderUsers { get; set; }
        public DbSet<DateInfo> DateInfos { get; set; }
        public DbSet<OrderTaskStatus> TaskStatuses { get; set; }
        public DbSet<UserContacts> UserContacts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
