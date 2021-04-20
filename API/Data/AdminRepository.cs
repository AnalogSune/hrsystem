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
        private readonly IFileService _fileService;

        public AdminRepository(DataContext context, IMapper mapper, IFileService fileService)
        {
            _fileService = fileService;
            _mapper = mapper;
            _context = context;
        }
        public async Task<bool> DepartmentExists(DepartmentDto department)
        {
            return await _context.Departments.AnyAsync(d => d.Name == department.Name);
        }
        public async Task<Department> CreateDepartment(DepartmentDto department)
        {
            await _context.Roles.AddRangeAsync(department.DepartmentRoles);
            Department newDepartment = _mapper.Map<Department>(department);
            await _context.Departments.AddAsync(newDepartment);

            if (await _context.SaveChangesAsync()>0)
                return newDepartment;

            return null;
        }

        public async Task<Department> UpdateDepartment(int departmentId, DepartmentDto department)
        {
            var depToUpdate = _context.Departments
                .Where(d => d.Id == departmentId)
                .Include(d => d.DepartmentRoles)
                .FirstOrDefault();
            _mapper.Map(department, depToUpdate);

            if (await _context.SaveChangesAsync() > 0)
                return depToUpdate;

            return null;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            Department depToDelete = await _context.Departments
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            if (depToDelete != null)
            {
                _context.Remove<Department>(depToDelete);
                if (await _context.SaveChangesAsync() > 0)
                    return true;
            }

            return false;
        }

        public async Task<bool> DeleteUser(int id)
        {
            AppUser userToDelete = await _context.Users
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            if (userToDelete != null)
            {
                _context.Remove<AppUser>(userToDelete);
                if (await _context.SaveChangesAsync() > 0)
                {
                    await _fileService.DeleteFolderAsync(userToDelete.Email);
                    return true;
                }
            }

            return false;
        }
    }
}