using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CVController : BaseApiController
    {
        private readonly IAuthRepository _authRepository;
        private readonly ICVRepository _cvRepository;
        private readonly IFileService _fileService;
        public CVController(IAuthRepository authRepository, ICVRepository cvRepository, IFileService fileService)
        {
            _fileService = fileService;
            _cvRepository = cvRepository;
            _authRepository = authRepository;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> SendCV([FromForm] CVDto cvDto)
        {

            var result = await _fileService.AddFileAsync(cvDto.CvFile, "CVs");
            return Ok(await _cvRepository.AddCVEntry(cvDto, result.Url.ToString(), result.PublicId));
        }
    }
}