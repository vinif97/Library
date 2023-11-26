namespace Library.Application.Services.Interfaces
{
    public interface IImageDeleteService
    {
        Task<bool> Delete(string resourceId);
    }
}
