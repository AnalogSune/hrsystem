using System.Collections.Generic;
using System.Configuration;
using API.Data;
using API.Helper;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
            services.AddSingleton<IMailService, MailService>();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
            
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer("Server=DESKTOP-PSMGHEA\\SQLEXPRESS;Database=HrSystem;User Id=Analog;Password=aq2b4ugef;");
                // options.UseMySql(config.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new System.Version(10, 4, 17)));
            });

            return services;
        }
    }
}