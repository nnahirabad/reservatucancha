using canchasfutbol.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<Usuario>
    {
        // Estos metodos son los que serviran luego para hacer las validaciones.
        Task<Usuario?> GetByEmailAsync(string email);

        Task<bool> ExistByUsername(string username);

        Task<Usuario?> GetByName(string username);


    }
}
