using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Library.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Services
{
    public class CloudinaryImageUploadService : IImageUploadService
    {
        private Cloudinary Cloudinary;
        private string CloudName = "dodxs9dph";
        private string ApiKey = "683212422911247";
        private string ApiSecret = "9yCXWsoT955FDG5LAKe7d1FyreY";

        public CloudinaryImageUploadService()
        {
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

            var result = Cloudinary.Upload(uploadparams);

            if (result.Error != null)
            {
                throw new Exception($"Error while uploading image: {result.Error.Message}");
            }

            return result.SecureUrl.ToString();

        }
    }
}
