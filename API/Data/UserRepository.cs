using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDto> GetUser(int id)
        {
            return await _context.Users.Where(u => u.Id == id).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetUsers()
        {
            return await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetUsersWithRole(int roleId)
        {
            return await _context.Roles.Where(r => r.Id == roleId).Include(p => p.Employees)
                .SelectMany(s => s.Employees).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetUsersWithDepartment(int departmentId)
        {
            return await _context.departments.Where(d => d.Id == departmentId).Include(p => p.Employees)
                .SelectMany(s => s.Employees).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
        
        public async Task<IEnumerable<MemberDto>> GetUsersWithParameters(UserFilterDto filters)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Address))
            {
                users = users.Where(u => u.Address.Contains(filters.Address));
            }
            
            if (!string.IsNullOrEmpty(filters.FName))
            {
                users = users.Where(u => u.FName.Contains(filters.FName));
            }
            
            if (!string.IsNullOrEmpty(filters.LName))
            {
                users = users.Where(u => u.LName.Contains(filters.LName));
            }
            
            if (!string.IsNullOrEmpty(filters.PhoneNumber))
            {
                users = users.Where(u => u.PhoneNumber.Contains(filters.PhoneNumber));
            }
            
            if (!string.IsNullOrEmpty(filters.Email))
            {
                users = users.Where(u => u.Email.Contains(filters.Email));
            }

            return await users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}