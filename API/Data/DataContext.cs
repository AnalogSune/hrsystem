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
        public DbSet<Dashboard> Dashboards { get; set; }

        public DbSet<DaysOffRequest> DaysOffRequests { get; set; }

        public DbSet<WorkHomeRequest> WorkHomeRequests { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Recruitment> Recruitments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasOne(s => s.Role)
                .WithMany(e => e.Employees)
                .HasForeignKey(k => k.RoleId)
                .OnDelete(DeleteBehavior.SetNull);
                
            builder.Entity<AppUser>()
                .HasOne(s => s.InDepartment)
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

            builder.Entity<Role>()
                .HasOne(d => d.InDepartment)
                .WithMany(r => r.DepartmentRoles)
                .HasForeignKey(k => k.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkHomeRequest>()
                .HasOne(d => d.Employee)
                .WithMany(r => r.WorkHomeRequests)
                .HasForeignKey(k => k.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
