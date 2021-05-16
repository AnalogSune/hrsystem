using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet.Actions;
using System;

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
            return await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.InDepartment)
                .Include(u => u.InDepartment.DepartmentRoles)
                .Include(u => u.Role)
                .Select(u => _mapper.Map<MemberDto>(u))
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetUsers()
        {
            return await _context.Users
                .Include(e => e.InDepartment)
                .Include(e => e.Role)
                .Select(e => _mapper.Map<MemberDto>(e))
                .ToListAsync();
        }

        public async Task<bool> ChangeUserDepartment(int userId, int departmentId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user==null) return false;
            user.DepartmentId = departmentId;
            return await _context.SaveChangesAsync() > 0;
        }

         public async Task<bool> ChangeUserRole(int userId, int roleId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user==null) return false;
            user.RoleId = roleId;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<MemberDto>> GetUsersWithRole(int roleId)
        {
            return await _context.Roles
                .Where(r => r.Id == roleId)
                .Select(e => _mapper.Map<MemberDto>(e))
                .ToListAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetUsersWithPending()
        {
            return await _context.Requests
                .Include(r => r.Employee)
                .Select(r => r.Employee)
                .Distinct()
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetUsersWithDepartment(int departmentId)
        {
            return await _context.Users
                .Where(d => d.DepartmentId == departmentId)
                .Include(e => e.InDepartment)
                .Include(e => e.Role)
                .Select(e => _mapper.Map<MemberDto>(e))
                .ToListAsync();
        }

        public async Task<MemberDto> UpdateUser(MemberDto userEdit)
        {
            var user = _context.Users
                .Where(u => u.Id == userEdit.Id)
                
                .FirstOrDefault();
            var u = _context.Entry(user);
            user = _mapper.Map<AppUser>(userEdit);
            // user.DepartmentId = userEdit.InDepartment?.Id;
            // user.RoleId = userEdit.Role?.Id;
            u.State = EntityState.Modified;
            
            if (await _context.SaveChangesAsync() > 0)
                return _mapper.Map<MemberDto>(user);

            return null;
        }

        public async Task<bool> ChangeImage(int id, string url, string publicId)
        {
            var user = _context.Users
                .Where(u => u.Id == id)
                .FirstOrDefault();

            user.PictureId = publicId;
            user.PictureUrl = url;
           
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UploadFile(int id, UploadResult file, string originalFilename, string contentType)
        {
            PersonalFile newFile = new PersonalFile
            {
                FileOwnerId = id,
                FileUrl = file.Url.ToString(),
                FileId = file.PublicId,
                FileType = contentType,
                OriginalFileName = originalFilename
            };

            await _context.PersonalFiles.AddAsync(_mapper.Map<PersonalFile>(newFile));

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RenameFileAsync(PersonalFilesDto personalFilesDto)
        {
            var file = await _context.PersonalFiles.Where(f => f.Id == personalFilesDto.Id).FirstOrDefaultAsync();
            file.OriginalFileName = personalFilesDto.OriginalFileName;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFileAsync(int fileId)
        {
            var file = await _context.PersonalFiles.Where(f => f.Id == fileId).FirstOrDefaultAsync();
            _context.Remove(file);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<PersonalFilesDto>> GetFiles(int id)
        {
            return await _context.Users
            .Where(u => u.Id == id)
            .Include(u => u.PersonalFiles)
            .SelectMany(f => f.PersonalFiles)
            .ProjectTo<PersonalFilesDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }    
        
        public async Task<PersonalFile> GetFile(int fileId)
        {
            return await _context.PersonalFiles.Where(f => f.Id == fileId).FirstOrDefaultAsync();
        }  

        public async Task<IEnumerable<MemberDto>> GetUsersWithParameters(UserFilterDto filters)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Address))
            {
                users = users
                    .Where(u => u.Address
                    .Contains(filters.Address));
            }
            
            if (!string.IsNullOrEmpty(filters.FName))
            {
                users = users
                    .Where(u => u.FName
                    .Contains(filters.FName));
            }
            
            if (!string.IsNullOrEmpty(filters.LName))
            {
                users = users
                    .Where(u => u.LName
                    .Contains(filters.LName));
            }
            
            if (!string.IsNullOrEmpty(filters.PhoneNumber))
            {
                users = users
                    .Where(u => u.PhoneNumber
                    .Contains(filters.PhoneNumber));
            }
            
            if (!string.IsNullOrEmpty(filters.Email))
            {
                users = users
                    .Where(u => u.Email
                    .Contains(filters.Email));
            }

            return await users
                .Select(u => _mapper.Map<MemberDto>(u))
                .ToListAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetUsersWithSingleParameters(string searchParam)
        {
            var users = _context.Users.AsQueryable();

            return await users
                .Where(u => u.FName.Contains(searchParam) || u.LName.Contains(searchParam) || u.Email.Contains(searchParam))
                .Select(u => _mapper.Map<MemberDto>(u))
                .Take(5)
                .ToListAsync();
        }
    }
}