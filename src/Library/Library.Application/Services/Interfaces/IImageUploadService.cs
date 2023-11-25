using Microsoft.AspNetCore.Http;

namespace Library.Application.Services.Interfaces
{
    public interface IImageUploadService
    {
        Task<string> Upload(IFormFile file);
    }
}
