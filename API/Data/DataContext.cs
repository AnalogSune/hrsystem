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
        public DbSet<PersonalFile> PersonalFiles { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
                builder.Entity<Tasks>()
                .HasOne(t => t.Employee)
                .WithMany()
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Tasks>()
                .HasMany(t => t.SubTasks)
                .WithOne()
                .HasForeignKey(t => t.TasksId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Request>()
                .Property(d => d.DateCreated)
                .HasPrecision(0);

            builder.Entity<Dashboard>()
                .Property(d => d.TimeCreated)
                .HasPrecision(0);

            builder.Entity<Dashboard>()
                .HasOne(s => s.Publisher)
                .WithMany()
                .HasForeignKey(k => k.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Request>()
                .HasOne(s => s.Employee)
                .WithMany()
                .HasForeignKey(k => k.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PersonalFile>()
                .HasOne(s => s.FileOwner)
                .WithMany(p => p.PersonalFiles)
                .HasForeignKey(k => k.FileOwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<AppUser>()
                .HasOne(e => e.InDepartment)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            
            builder.Entity<Department>()
                .HasIndex(r => r.Name)
                .IsUnique();

            builder.Entity<CalendarEntry>()
                .HasOne(w => w.Shift)
                .WithMany()
                .HasForeignKey(w => w.ShiftId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
