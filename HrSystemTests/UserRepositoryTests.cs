using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Xunit;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace HrSystemTests
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _repo;
        private readonly Mock mock;
        
        public UserRepositoryTests()
        {
            mock = MockFactory.CreateMemoryDb();

            mock.DataContext.Users.AddRangeAsync(
                Users()
            );
            
            mock.DataContext.SaveChangesAsync();
            _repo = new UserRepository(mock.DataContext, mock.Mapper);
        }

        private static IEnumerable<AppUser> Users()
        {
            yield return new AppUser()
            {
                Email = "sune_analog@yahoo.gr", 
                PictureUrl = String.Empty,
                PictureId = String.Empty,
                FName = "Kostas", 
                LName = "Ang",
                Address = "Agoratou 4",
                Country = "Greece",
                Nationality = "Greek",
                PhoneNumber = "123456767",
                DateOfBirth = new DateTime(2021, 05 ,5),
                DaysOffLeft = 2.2d,
                WorkedFromHome = 5,
                IsAdmin = true
            };
            
            yield return new AppUser()
            {
                Email = "user2@yahoo.gr", 
                PictureUrl = String.Empty,
                PictureId = String.Empty,
                FName = "user2", 
                LName = "user2",
                Address = "user2",
                Country = "user2",
                Nationality = "user2",
                PhoneNumber = "user2",
                DateOfBirth = new DateTime(2021, 05 ,5),
                DaysOffLeft = 2.2d,
                WorkedFromHome = 5,
                IsAdmin = true
            };
        }
        
        public static IEnumerable<object[]> Members()
        {
            yield return new object[]
            {
                new MemberDto() {
                    Id = 1, 
                    Email = "sune_analog@yahoo.gr", 
                    PictureUrl = String.Empty,
                    PictureId = String.Empty,
                    FName = "Kostas", 
                    LName = "Ang",
                    Address = "Agoratou 4",
                    Country = "Greece",
                    Nationality = "Greek",
                    PhoneNumber = "123456767",
                    DateOfBirth = new DateTime(2021, 05 ,5),
                    DaysOffLeft = 2.2d,
                    IsAdmin = true}
            };
            
            yield return new object[]
            {
                new MemberDto() {
                    Id = 2, 
                    Email = "user2@yahoo.gr", 
                    PictureUrl = String.Empty,
                    PictureId = String.Empty,
                    FName = "user2", 
                    LName = "user2",
                    Address = "user2",
                    Country = "user2",
                    Nationality = "user2",
                    PhoneNumber = "user2",
                    DateOfBirth = new DateTime(2021, 05 ,5),
                    DaysOffLeft = 2.2d,
                    IsAdmin = true}
            };
        }

        [Theory]
        [MemberData(nameof(Members))]
        public async Task getUser_ShouldReturnUser_IfExistsTheory(MemberDto expected)
        {

            var result = await _repo.GetUser(expected.Id);
            
            //Assert
            
            Assert.NotNull(result);
            Assert.Equal(result, expected);
        }
        
        [Fact]
        public async Task getUsers_ShouldReturnAllUsers()
        {

            var result = await _repo.GetUsers();
            
            //Assert
            
            Assert.NotNull(result);
            Assert.Equal(result, Members().Select(o => o[0]));
        }
    }
}