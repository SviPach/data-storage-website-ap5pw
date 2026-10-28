namespace data_storage_website_ap5pw.Application.Abstraction
{
    public interface IUserAppService
    {
        Task<(bool Success, string Message)> DeleteUserAsync(int userId);
    }
}