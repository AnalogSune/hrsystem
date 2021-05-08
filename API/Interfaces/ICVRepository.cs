using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ICVRepository
    {
        Task<bool> AddCVEntry(CVCreationDto cvDto);

        Task<bool> UpdateCVEntry(UpdateCVDto updateCVDto);

        Task<bool> DeleteCVEntry(int id);

        Task<IEnumerable<CV>> GetCVs();
    }
}