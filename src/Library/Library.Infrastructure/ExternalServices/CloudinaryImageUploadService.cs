using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Library.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Library.Application.Options;

namespace Library.Infrastructure.ExternalServices
{
    public class CloudinaryImageUploadService : IImageUploadService
    {
        private readonly Cloudinary Cloudinary;
        private readonly string CloudName;
        private readonly string ApiKey;
        private readonly string ApiSecret;

        public CloudinaryImageUploadService(IOptions<CloudinaryOptions> cloudinaryOptions)
        {
            CloudName = cloudinaryOptions.Value.CloudName;
            ApiKey = cloudinaryOptions.Value.ApiKey;
            ApiSecret = cloudinaryOptions.Value.ApiSecret;

            var account = new Account(CloudName, ApiKey, ApiSecret);
            Cloudinary = new Cloudinary(account);
            Cloudinary.Api.Secure = true;
        }

        public async Task<string> Upload(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var uploadparams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, memoryStream),
            };

            var result = await Cloudinary.UploadAsync(uploadparams);

            if (result.Error != null)
            {
                throw new Exception($"Error while uploading image: {result.Error.Message}");
            }

            return result.SecureUrl.ToString();
        }
    }
}
