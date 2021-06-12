using System;
using System.Threading.Tasks;
using Xunit;
using API.Data;
using API.DTOs;
using API.Helper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Tests
{
    public class Mock
    {
        public DataContext DataContext { get; }
        public IMapper Mapper { get; }

        public Mock()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            Mapper = mappingConfig.CreateMapper();
            DataContext = new DataContext(new DbContextOptionsBuilder<DataContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options);
        }
    }
}