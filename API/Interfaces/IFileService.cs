using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
    public interface IFileService
    {
        Task<CreateFolderResult> CreateFolderAsync(string name);
        string GetFolderUrl(string name);
        Task<DeleteFolderResult> DeleteFolderAsync(string name);
        Task<UploadResult> AddFileAsync(IFormFile file, string folder);

        Task<DeletionResult> DeleteFileAsync (string publicId, ResourceType type);
    }
}