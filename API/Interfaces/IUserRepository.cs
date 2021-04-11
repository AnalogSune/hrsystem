using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<MemberDto>> GetUsers();

        Task<MemberDto> GetUser(int id);
    }
}