using System;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HrSystemTests
{
    public class UserRepositoryTests
    {
                    
        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
        
        private readonly UserRepository _sut;
        private readonly Mock<DataContext> _contextMock = new Mock<DataContext>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();

        public UserRepositoryTests()
        {
            var usersMock = _userRepoMock.Object;
            var mapperMock = _mapperMock.Object;
            var contextMock = _contextMock.Object;

            _sut = new UserRepository(contextMock, mapperMock);
        }
        
        [Fact]
        public async Task getUser_ShouldReturnUser_IfExists()
        {
            //Arrange
            
        }
    }
}