using System.Threading.Tasks;
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
        public DbSet<CalendarEntry> Calendar { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<CV> CVs { get; set; }

        public DbSet<PersonalFiles> personalFiles { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<EmployeesTasks> EmployeesTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<EmployeesTasks>()
                .HasKey(k => new {k.EmployeeId, k.TaskId});

            builder.Entity<Request>()
                .Property(d => d.DateCreated)
                .HasPrecision(0);

            builder.Entity<Dashboard>()
                .Property(d => d.TimeCreated)
                .HasPrecision(0);

            builder.Entity<CalendarEntry>()
                .HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

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
                .WithMany()
                .HasForeignKey(k => k.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Role>()
                .HasOne(d => d.InDepartment)
                .WithMany(r => r.DepartmentRoles)
                .HasForeignKey(k => k.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Request>()
                .HasOne(s => s.Employee)
                .WithMany()
                .HasForeignKey(k => k.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PersonalFiles>()
                .HasOne(s => s.FileOwner)
                .WithMany(p => p.PersonalFiles)
                .HasForeignKey(k => k.FileOwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
