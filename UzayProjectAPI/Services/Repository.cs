using Microsoft.EntityFrameworkCore;
using UzayProjectAPI.Model;

namespace UzayProjectAPI.Services
{
    public class Repository:IRepository
    {
        private readonly ProjeContext _context;

        public Repository(ProjeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
