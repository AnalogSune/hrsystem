using System;
using System.IO;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services
{
    public enum DepartmentActionType {
        Create, Delete
    }

    public class LogService : ILogService
    {
        public async Task AcceptRequestLogFile(string user, RequestsDto requestsDto, string adminEmail)
        {
            try {

                string log = "Request by: " + user + ". Request for " + requestsDto.requestType + " at -> " + requestsDto.Date + 
                ". Duration: " + requestsDto.Duration + " days." + (requestsDto.Status == 1?" Accepted":" Declined") + " by: " + adminEmail + " -> " + DateTime.Now;

                using StreamWriter file = new("logFiles/Requestslogs.txt", append: true);
                await file.WriteLineAsync(log);
            } catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task LoginLogFile(LoginDto loginDto)
        {
            try {
                string log = "User: " + loginDto.Email + " logged in -> " + DateTime.Now;

                using StreamWriter file = new("logFiles/Accesslogs.txt", append: true);
                await file.WriteLineAsync(log);
            } catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task RegisterLogFile(RegisterDto registerDto, string adminEmail)
        {
            try {
                string log = "User: " + registerDto.Email + " " + registerDto.FName + " " 
                + registerDto.LName + " registered by: " + adminEmail + " -> " + DateTime.Now;

                using StreamWriter file = new("logFiles/Registerlogs.txt", append: true);
                await file.WriteLineAsync(log);
            } catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task RequestMadeLogFile(string user, RequestsDto requestsDto)
        {
            try {

                string log = "Request made by: " + user + ". Request for " + requestsDto.requestType + " at -> " + requestsDto.Date + 
                ". Duration: " + requestsDto.Duration + " days. -> " + DateTime.Now;

                using StreamWriter file = new("logFiles/Requestslogs.txt", append: true);
                await file.WriteLineAsync(log);
            } catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task UserDeletedLogFile(string user, string admin)
        {
            try {

                string log = "User: " + user + " was deleted by " + admin + " -> " + DateTime.Now;

                using StreamWriter file = new("logFiles/Userlogs.txt", append: true);
                await file.WriteLineAsync(log);
            } catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task DepartmentsLogFile(DepartmentDto department, string admin, DepartmentActionType actionType)
        {
            try {
                string log = "Department " + department.Name + " was " + (actionType == 0?"created by ":"deleted by ") + admin + " -> " + DateTime.Now;

                using StreamWriter file = new("logFiles/Departmentslogs.txt", append: true);
                await file.WriteLineAsync(log);
            } catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}