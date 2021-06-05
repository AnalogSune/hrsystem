using System.Threading.Tasks;
using API.Interfaces;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using System.Linq;
using System;
using System.Collections.Generic;

namespace API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IMailService _mailService;

        public AuthRepository(DataContext context, ITokenService tokenService, IMapper mapper, IFileService fileService,
            IMailService mailService)
        {
            _fileService = fileService;
            _mailService = mailService;
            _mapper = mapper;
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<UserDto> Login(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == username);

            if (user == null) return null;

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != user.PasswordHash[i]) return null;
            }

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();

            var user = _mapper.Map<AppUser>(registerDto);
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<bool> ChangePassword(int id, string newPassword)
        {
            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null) return false;
            
            using (var hmac = new HMACSHA512())
            {
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
                user.PasswordSalt = hmac.Key;
            }

            return await _context.SaveChangesAsync() > 0;
        } 
        

        public async Task<bool> UserExists(string username) => await _context.Users.AnyAsync(x => x.Email == username.ToLower());
        public async Task<bool> IsAdmin(int id) => (await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync()).IsAdmin == true;

        public async Task<string> GetEmailById(int id) 
        {
            string userEmail = await _context.Users
                .Where(x => x.Id == id)
                .Select(x => x.Email)
                .SingleOrDefaultAsync();
            return userEmail;
        }

        public async Task<bool> ChangePasswordEmail(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
            if (user == null) return false;

            var token = _tokenService.CreateSimpleToken(user);

            await _mailService.SendMessage("HR System: Password change",
            "<!DOCTYPE html><p><h1>You requested a password change.</h1></p>" +
            $"<p><a href='https://localhost:5001/password?token={token}'>Click here to change your password!</a></p>",
            email,
            user.FName + " " + user.LName);

            return true;
        }
    }
}