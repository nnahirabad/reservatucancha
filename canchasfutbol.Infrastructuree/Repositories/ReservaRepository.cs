using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace canchasfutbol.Infrastructuree.Repositories
{
    public class ReservaRepository : RepositoryBase<Reserva>, IReservaRepository
    {
        private readonly AppDbContext _context;
        public ReservaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Reserva> GetReservaByUsername(string username)
        {
           var reserva = _context.Reservas.Where(r => r.User.Username == username).FirstOrDefaultAsync();
            return reserva!;
        }

        public Task<Reserva> GetReservaByDay(DateOnly day)
        {
          var reserva =  _context.Reservas.Where(r => r.Fecha == day).FirstOrDefaultAsync();
            return reserva!;
        }

         public async Task<bool> ExistsOverlap(Guid canchaId, DateOnly fecha, TimeOnly? horaInicio, TimeOnly horaFin)
        {
            return await _context.Reservas.AnyAsync(r =>
                r.CanchaId == canchaId &&
                r.Fecha == fecha 
                && horaInicio < r.HoraFin && horaFin > r.HoraInicio
            );
            
        }

        public async Task<List<Reserva>> GetReservasByCanchaAndDate(Guid canchaId, DateOnly fecha)
        {
            return await _context.Reservas
                .Where(r => r.CanchaId == canchaId && r.Fecha == fecha)
                .OrderBy(r => r.HoraInicio)
                .ToListAsync();
        }
    }
}
