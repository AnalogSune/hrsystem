using System;
using System.Threading.Tasks;
using Xunit;
using API.Data;
using API.DTOs;
using API.Helper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HrSystemTests
{
    public class Mock
    {
        public DataContext DataContext { get; }
        public IMapper Mapper { get; }

        public Mock(DataContext data, IMapper mapper)
        {
            DataContext = data;
            Mapper = mapper;
        }
    }

    public static class MockFactory
    {
        public static Mock CreateMemoryDb()
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfiles()); });

            var mapper = mappingConfig.CreateMapper();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .Options;
            var dbContext = new DataContext(options);
            return new Mock(dbContext, mapper);
        }

        public static Mock CreateMySqlDb()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfiles()); });
            var mapper = mappingConfig.CreateMapper();
            var dbContext = new DataContext(new DbContextOptionsBuilder<DataContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options);
            return new Mock(dbContext, mapper);
        }
    }
}