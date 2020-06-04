using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DAL.Entities.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.Base
{
    internal class RestrauntTrackerContext : IdentityDbContext<ApplicationUser>
    {
        public RestrauntTrackerContext(DbContextOptions<RestrauntTrackerContext> options)
            : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderTask> Tasks { get; set; }
        public DbSet<Staff> OrderUsers { get; set; }
        public DbSet<DateInfo> DateInfos { get; set; }
        public DbSet<OrderTaskStatus> TaskStatuses { get; set; }
        public DbSet<UserContacts> UserContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
