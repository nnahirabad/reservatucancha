using canchasfutbol.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Contracts.Persistence
{
    public interface IReservaRepository : IAsyncRepository<Reserva>
    {

        

         Task<Reserva> GetReservaByUsername(string username);

        Task<Reserva> GetReservaByDay(DateOnly day);

        Task<bool> ExistsOverlap(Guid canchaId, DateOnly fecha, TimeOnly? horaInicio, TimeOnly horaFin);

        Task<List<Reserva>> GetReservasByCanchaAndDate(Guid canchaId, DateOnly fecha);
    }
}
