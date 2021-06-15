using API.Data;
using API.Helper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HrSystemTests
{
    public class MockDependencies
    {
        public DataContext DataContext { get; }
        public IMapper Mapper { get; }

        public MockDependencies(DataContext data, IMapper mapper)
        {
            DataContext = data;
            Mapper = mapper;
        }
    }

    public static class MockDependenciesFactory
    {
        public static MockDependencies CreateMemoryDb()
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfiles()); });

            var mapper = mappingConfig.CreateMapper();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .Options;
            var dbContext = new DataContext(options);
            return new MockDependencies(dbContext, mapper);
        }

        public static MockDependencies CreateMySqlDb()
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
            return new MockDependencies(dbContext, mapper);
        }
    }
}