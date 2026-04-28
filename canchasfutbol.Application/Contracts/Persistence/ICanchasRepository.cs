

using canchasfutbol.Domain.Models;

namespace canchasfutbol.Application.Contracts.Persistence
{
    public interface ICanchasRepository : IAsyncRepository<Cancha>
    {


        Task<Cancha> GetCanchaByName (string name);
        


      

    }
}
