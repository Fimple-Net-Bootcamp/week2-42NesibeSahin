namespace UzayProjectAPI.Services
{
    public interface IRepository
    {
        Task<bool> SaveChangesAsync();
    }
}
