using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class RequestsRepository : IRequestsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RequestsRepository(DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> CreateDayOffRequest(RequestsDto requestsDto)
        {
            await _context.DaysOffRequests.AddAsync(_mapper.Map<DaysOffRequest>(requestsDto));
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> CreateWorkFromHomeRequest(RequestsDto requestsDto)
        {
            await _context.WorkHomeRequests.AddAsync(_mapper.Map<WorkHomeRequest>(requestsDto));
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> StatusWorkHomeRequest(int id, int status)
        {
            var req = await _context.WorkHomeRequests.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (req == null) return false;

            req.Status = status;
            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }
        
        public async Task<bool> StatusDayOffRequest(int id, int status)
        {
            var req = await _context.DaysOffRequests.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (req == null) return false;

            req.Status = status;
            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}