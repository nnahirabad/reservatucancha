using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Usuarios.Queries.GetUserById
{
    public class UserVm
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Rol { get; set; } = null!;
    }
}
