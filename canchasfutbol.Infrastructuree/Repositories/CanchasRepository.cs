

using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Domain.Models;
using canchasfutbol.Infrastructuree.Repositories;
using Microsoft.EntityFrameworkCore;

namespace canchasfutbol.Infrastructuree.Services
{
    public class CanchasRepository : RepositoryBase<Cancha>, ICanchasRepository
    {
        private readonly AppDbContext _dbContext;

        public CanchasRepository(AppDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<Cancha> GetCanchaByName(string name)
        {
            var cancha = await _dbContext.Canchas.Where(r => r.Name == name).FirstOrDefaultAsync();
            return cancha!; 
        }



    }
}
