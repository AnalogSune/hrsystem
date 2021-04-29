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
    public class RequestsRepository : IRequestsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RequestsRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> CreateRequest(RequestsDto requestsDto)
        {
            await _context.Requests.AddAsync(_mapper.Map<Request>(requestsDto));
            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> UpdateRequestStatus(int id, RequestStatus status)
        {
            var req = await _context.Requests.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (req == null) return false;

            req.Status = status;
            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }

         public async Task<ICollection<RequestsDto>> GetRequests(int id, RequestType? type, RequestStatus? status)
        {
            return await _context.Requests
                .Where(u => u.EmployeeId == id)
                .Where(s => (status == null ? true : s.Status == status))
                .Where(t => (type == null ? true : t.requestType == type))
                .Select(u => _mapper.Map<RequestsDto>(u))
                .ToListAsync();
        }
        public async Task<RequestsDto> GetRequest(int requestId)
        {
            return await _context.Requests.Where(r => r.Id == requestId).ProjectTo<RequestsDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<ICollection<RequestsDto>> GetRequests(RequestSearchDto searchDto)
        {
            return await _context.Requests
                .Where(u => (searchDto.EmployeeId == null ? true : u.EmployeeId == searchDto.EmployeeId))
                .Where(s => (searchDto.requestStatus == null ? true : s.Status == searchDto.requestStatus))
                .Where(t => (searchDto.requestType == null ? true : t.requestType == searchDto.requestType))
                .Select(u => _mapper.Map<RequestsDto>(u))
                .ToListAsync();
        }
    }
}