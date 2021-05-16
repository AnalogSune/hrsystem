using System;
using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CVController : BaseApiController
    {
        private readonly IAuthRepository _authRepository;
        private readonly ICVRepository _cvRepository;
        public CVController(IAuthRepository authRepository, ICVRepository cvRepository)
        {
            _cvRepository = cvRepository;
            _authRepository = authRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SendCV([FromForm] CVCreationDto cvDto)
        {
            return Ok(await _cvRepository.AddCVEntry(cvDto));
        }

        [HttpGet]

        public async Task<IActionResult> GetCVs()
        {
            return Ok(await _cvRepository.GetCVs());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCV(UpdateCVDto updateCVDto)
        {
            if (User.IsAdmin())
            {
                return Ok(await _cvRepository.UpdateCVEntry(updateCVDto));
            }

            return Unauthorized("You don't have the rights to do this!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCV(int id)
        {
            if (User.IsAdmin())
            {
                return Ok(await _cvRepository.DeleteCVEntry(id));
            }

            return Unauthorized("You don't have the rights to do this!");
        }
    }
}