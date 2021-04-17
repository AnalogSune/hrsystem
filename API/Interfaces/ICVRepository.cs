using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface ICVRepository
    {
        Task<bool> AddCVEntry(CVDto cvDto, string Url, string id);
    }
}