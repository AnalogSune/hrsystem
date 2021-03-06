using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IRequestsRepository
    {
        Task<bool> CreateRequest(RequestsDto requestsDto);

        Task<bool> UpdateRequestStatus(int id, RequestStatus status);
        Task<ICollection<RequestsDto>> GetRequests(int id, RequestType? type, RequestStatus? status);
        Task<ICollection<RequestsDto>> GetRequests(RequestSearchDto searchDto);
        Task<RequestsDto> GetRequest(int requestId);

        Task<string> getUserByRequestId (int id);
    }
}