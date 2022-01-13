using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Xunit;
using Xunit.Abstractions;

namespace HrSystemTests
{
    public class UserData : IEnumerable<object[]>
    {
        public List<AppUser> _users;
        public UserRepository _repo;
        private MockDependencies _mockDependencies;
        
        
        public UserData()
        {
            _mockDependencies = MockDependenciesFactory.CreateMemoryDb();
            var deps = PopulateDb.PopulateDepartments();
            _mockDependencies.DataContext.Departments.AddRange(deps);
            _users = PopulateDb.PopulateUsers(deps);
            foreach (var dep in deps)
            {
                _mockDependencies.DataContext.Roles.AddRange(dep.DepartmentRoles);
            }
            _mockDependencies.DataContext.Users.AddRange(_users);
            _mockDependencies.DataContext.SaveChanges();
            _repo = new UserRepository(_mockDependencies.DataContext, _mockDependencies.Mapper);
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var t in _users)
            {
                yield return new object[] {_mockDependencies.Mapper.Map<MemberDto>(t), t, _repo };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class UserRepositoryTests
    {
        private readonly UserRepository _repo;
        private static MockDependencies _mockDependencies;
        private readonly ITestOutputHelper _outputHelper;
        
        public UserRepositoryTests()
        {
            _mockDependencies = MockDependenciesFactory.CreateMemoryDb();
            var deps = PopulateDb.PopulateDepartments();
            _mockDependencies.DataContext.SaveChangesAsync();
            _repo = new UserRepository(_mockDependencies.DataContext, _mockDependencies.Mapper);
        }


        [Theory]
        [ClassData(typeof(UserData))]
        public async Task getUser_ShouldReturnUser_IfExistsTheory(MemberDto expected, AppUser given, UserRepository repo)
        {

            var result = await repo.GetUser(given.Id);
            
            Assert.NotNull(result);
            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task getUsers_ShouldReturnAllUsers()
        {
            UserData users = new UserData();

            var result = await users._repo.GetUsers();
            
            //Assert
            
            Assert.NotNull(result);
            Assert.Equal(result, users.Select(o => o[0]));
        }
        
        [Fact]
        public async Task GetUsersWithSingleParameters_Test()
        {
            UserData users = new UserData();

            var result = await users._repo.GetUsersWithSingleParameters("Vivian");
            
            //Assert
            
            Assert.NotNull(result);
            Assert.Equal(result, users.Select(o => o[0]));
        }
    }
}