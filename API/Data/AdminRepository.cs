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

        public async Task<bool> UpdateDepartment(int departmentId, DepartmentDto department)
        {
            var depToUpdate = _context.departments.Where(d => d.Id == departmentId).Include(d => d.DepartmentRoles).FirstOrDefault();
            _mapper.Map(department, depToUpdate);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            
            return false;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            Departments depToDelete = await _context.departments.Where(d => d.Id == id).FirstOrDefaultAsync();
            _context.Remove<Departments>(depToDelete);
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}