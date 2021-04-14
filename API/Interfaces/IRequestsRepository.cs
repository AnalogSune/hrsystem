using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IRequestsRepository
    {
        Task<bool> CreateWorkFromHomeRequest(RequestsDto requestsDto);

        Task<bool> CreateDayOffRequest(RequestsDto requestsDto);
        Task<bool> StatusDayOffRequest(int id, int status);
        Task<bool> StatusWorkHomeRequest(int id, int status);
    }
}