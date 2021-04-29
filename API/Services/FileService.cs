using System;
using System.Threading.Tasks;
using API.Helper;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class FileService : IFileService
    {
        private readonly Cloudinary _cloudinary;

        public FileService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<CreateFolderResult> CreateFolderAsync(string name)
        {
            return await _cloudinary.CreateFolderAsync(name);
        }

        public string GetFolderUrl(string name)
        {
            return _cloudinary.DownloadFolder(name);
        }

        public async Task<DeleteFolderResult> DeleteFolderAsync(string name)
        {
            return await _cloudinary.DeleteFolderAsync(name);
        }

        public async Task<UploadResult> AddFileAsync(IFormFile file, string folder)
        {
            var uploadResult = new RawUploadResult();
            if (file.Length > 0 && file.Length < 20971520)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = folder
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeleteFileAsync(string publicId, ResourceType type)
        {
            var deleteParams = new DeletionParams(publicId);
            deleteParams.ResourceType = type;            
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}