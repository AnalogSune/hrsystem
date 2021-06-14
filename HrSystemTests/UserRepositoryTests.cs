using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Helper;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HrSystemTests
{
    public class UserRepositoryTests
    {
        private IMapper Mapper { get; }
        private readonly UserRepository _repo;

        public UserRepositoryTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            
            Mapper = mappingConfig.CreateMapper();
            
            var options = new DbContextOptionsBuilder<DataContext>()  
                .UseInMemoryDatabase("InMemoryDb")  
                .Options;  
            var dbContext = new DataContext(options);

            dbContext.Users.AddRangeAsync(
                new AppUser() {Id = 1, Email = "sune_analog@yahoo.gr", FName = "Kostas", LName = "Ang"},
                new AppUser() {Id = 2, Email = "user2", FName = "user2", LName = "user2"},
                new AppUser() {Id = 3, Email = "user3", FName = "user3", LName = "user3"}
            );
            
            dbContext.SaveChangesAsync();
            _repo = new UserRepository(dbContext, Mapper);
        }
        
        [Fact]
        public async Task getUser_ShouldReturnUser_IfExists()
        {
            //Arrange

            var expectedUser = new MemberDto()
            {
                Id = 2,
                Email = "user2",
                FName = "user2",
                LName = "user2"
            };
            
            //Act

            var result = await _repo.GetUser(2);
            
            //Assert
            
            Assert.NotNull(result);
            Assert.Equal(result, expectedUser);
        }
    }
}