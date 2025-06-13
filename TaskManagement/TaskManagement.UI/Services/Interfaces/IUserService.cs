namespace TaskManagement.UI.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<int>> GetAllUserIds();
        Task<List<string>> GetAllUsernames();
    }
}
