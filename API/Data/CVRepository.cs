using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CVRepository : ICVRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CVRepository(DataContext context, IMapper mapper, IFileService fileService)
        {
            _fileService = fileService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddCVEntry(CVCreationDto cvDto)
        {
            var result = await _fileService.AddFileAsync(cvDto.CvFile, "CVs");
            var entry = _mapper.Map<CV>(cvDto);
            entry.FileUrl = result.Url.ToString();
            entry.FileId = result.PublicId;
            await _context.CVs.AddAsync(entry);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCVEntry(UpdateCVDto updateCVDto)
        {
            var cvEntry = _context.CVs
                .Where(i => i.Id == updateCVDto.Id)
                .FirstOrDefault();

            cvEntry.AdminNote = updateCVDto.AdminNotes;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCVEntry(int id)
        {
            var cvEntry = _context.CVs
                .Where(i => i.Id == id)
                .FirstOrDefault();

            await _fileService.DeleteFileAsync(cvEntry.FileId, CloudinaryDotNet.Actions.ResourceType.Image);

            _context.CVs.Remove(cvEntry);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<CV>> GetCVs()
        {
            return await _context.CVs.ToListAsync();
        }
    }
}