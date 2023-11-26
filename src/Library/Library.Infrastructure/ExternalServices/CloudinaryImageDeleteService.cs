using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Library.Application.Services.Interfaces;
using Library.Application.Options;
using Microsoft.Extensions.Options;
using System.IO;

namespace Library.Infrastructure.ExternalServices
{
    public class CloudinaryImageDeleteService : IImageDeleteService
    {
        private readonly Cloudinary Cloudinary;
        private readonly string CloudName;
        private readonly string ApiKey;
        private readonly string ApiSecret;

        public CloudinaryImageDeleteService(IOptions<CloudinaryOptions> cloudinaryOptions)
        {
            CloudName = cloudinaryOptions.Value.CloudName;
            ApiKey = cloudinaryOptions.Value.ApiKey;
            ApiSecret = cloudinaryOptions.Value.ApiSecret;

            var account = new Account(CloudName, ApiKey, ApiSecret);
            Cloudinary = new Cloudinary(account);
            Cloudinary.Api.Secure = true;
        }

        public async Task<bool> Delete(string url)
        {
            string publicId = GetPublicIdFromUrl(url);

            var deletionParams = new DeletionParams(publicId);

            var result = await Cloudinary.DestroyAsync(deletionParams);

            if (result.Error != null)
            {
                throw new Exception($"Error while deleting image: {result.Error.Message}");
            }

            return true;
        }

        private static string GetPublicIdFromUrl(string url)
        {
            string[] splittedUrl = url.Split('/');
            string publicId = splittedUrl.Last().Split('.').First();

            return publicId;
        }
    }
}
