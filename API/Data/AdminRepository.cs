using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            // await _context.Roles.AddRangeAsync(department.DepartmentRoles);
            Department newDepartment = _mapper.Map<Department>(department);
            await _context.Departments.AddAsync(newDepartment);

            if (await _context.SaveChangesAsync()>0)
                return newDepartment;

            return null;
        }

        public async Task<Department> AddRole(int departmentId, string name)
        {
            await _context.Roles.AddAsync(new Role{
                RoleName = name,
                DepartmentId = departmentId
            });

            await _context.SaveChangesAsync();
               
            return await _context.Departments.Where(d => d.Id == departmentId).Include(d => d.DepartmentRoles).FirstOrDefaultAsync();
        }

        public async Task<Department> DeleteRole(int roleid)
        {
            var role = await _context.Roles.Where(r => r.Id == roleid).FirstOrDefaultAsync();
            var dep = await _context.Departments.Where(d =>d.Id == role.DepartmentId).Include(d => d.DepartmentRoles).FirstOrDefaultAsync();
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
                return dep;
            }

            return null;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            Department depToDelete = await _context.Departments
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            await _context.Users
                .Where(u => u.DepartmentId == depToDelete.Id)
                .ForEachAsync(u =>{ u.DepartmentId = null; u.RoleId = null;});

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

        public async Task<bool> AddPost(DashboardDto dashboardDto)
        {
            await _context.Dashboards.AddAsync(_mapper.Map<Dashboard>(dashboardDto));

            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<IEnumerable<DashboardReturnDto>> GetPosts()
        {
            return await _context.Dashboards
                .Include(d => d.Publisher)
                .OrderByDescending(d => d.TimeCreated)
                .ProjectTo<DashboardReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> DeletePost(int id)
        {
            var entry = await _context.Dashboards.Where(i => i.Id == id).FirstOrDefaultAsync();
            _context.Dashboards.Remove(entry);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _context.Departments.Include(d => d.DepartmentRoles).ToListAsync();
        }
    }
}