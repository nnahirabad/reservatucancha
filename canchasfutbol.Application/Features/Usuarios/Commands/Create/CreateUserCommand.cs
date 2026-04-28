

using canchasfutbol.Application.Dtos;
using canchasfutbol.Application.Dtos.Identity;
using MediatR;

namespace canchasfutbol.Application.Features.Usuarios.Commands.Create
{
    public class CreateUserCommand : IRequest<ResponseDto<AuthResponse>>
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!; 

        public string Rol { get; set; } = null!;

    }
}
