using API.Data;
using API.Helper;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Quartz;

namespace API.Extensions
{
    public static class ApplicationServiceExntensions
    {
        public static IServiceCollection addApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            
            services.AddScoped<IRequestsRepository, RequestsRepository>();
            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<ICVRepository, CVRepository>();
            services.AddSingleton<ILogService, LogService>();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(config.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new System.Version(10, 4, 17)));
            });

            return services;
        }
    }
}