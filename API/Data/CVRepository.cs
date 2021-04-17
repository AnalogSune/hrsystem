using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class CVRepository : ICVRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CVRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddCVEntry(CVDto cvDto, string Url, string id)
        {
            var entry = _mapper.Map<CV>(cvDto);
            entry.FileUrl = Url;
            entry.FileId = id;
            await _context.CVs.AddAsync(entry);
            return await _context.SaveChangesAsync() > 0;
        }
        
    }
}