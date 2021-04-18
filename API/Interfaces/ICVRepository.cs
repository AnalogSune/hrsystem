using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface ICVRepository
    {
        Task<bool> AddCVEntry(CVDto cvDto);

        Task<bool> UpdateCVEntry(UpdateCVDto updateCVDto);

        Task<bool> DeleteCVEntry(int id);
    }
}