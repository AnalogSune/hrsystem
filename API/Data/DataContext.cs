using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Dashboard> dashboards { get; set; }

        public DbSet<DaysOffRequest> DaysOffRequests { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasOne(s => s.Role)
                .WithMany(e => e.Employees)
                .HasForeignKey(k => k.RoleId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<AppUser>()
                .HasOne(s => s.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(k => k.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Dashboard>()
                .HasOne(s => s.Publisher)
                .WithMany(p => p.Posts)
                .HasForeignKey(k => k.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DaysOffRequest>()
                .HasOne(s => s.Employee)
                .WithMany(r => r.DaysOffRequests)
                .HasForeignKey(k => k.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
