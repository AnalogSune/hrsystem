using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AdminRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<bool> CreateDepartment(DepartmentDto department)
        {
            if (await _context.departments.AnyAsync(d => d.Department == department.Name)) return false;

            await _context.Roles.AddRangeAsync(department.DepartmentRoles);
            await _context.departments.AddAsync(_mapper.Map<Departments>(department));

            await _context.SaveChangesAsync();
            return true;
        }

    }
}