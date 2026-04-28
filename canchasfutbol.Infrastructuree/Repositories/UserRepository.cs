using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace canchasfutbol.Infrastructuree.Repositories
{
    public class UserRepository : RepositoryBase<Usuario>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> ExistByUsername(string username)
        {
            return await _context.Usuarios!.AnyAsync(u => u.Username == username);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios!.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetByName(string username)
        {
            return await _context.Usuarios!.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
