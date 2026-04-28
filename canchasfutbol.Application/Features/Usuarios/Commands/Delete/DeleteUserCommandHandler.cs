using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Usuarios.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private ILogger<DeleteUserCommandHandler> _logger;
        private IUnitOfWorkRepository _unit;

        public DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IUnitOfWorkRepository unitOwork)
        {
            _logger = logger;
            _unit = unitOwork; 
        }
        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // 1. Se busca el usuario con el ID que recibimos del request. 
            var existeUser = await _unit.UserRepository.GetByGuidAsync(request.Id);
            if (existeUser == null) {
                _logger.LogError("No existe usuario con ese ID");
                throw new NotFoundException(); 
            }

            //Si existe, se llama al repositorio y se elimina.
            _unit.UserRepository.DeleteEntity(existeUser);
            //Como usamos un metodo local, debemos usar el metodo Complete que actualiza los cambios async en la BD.
            var result = await _unit.Complete();
            if (result <= 0) {
                throw new Exception("No se pudo eliminar el usuario por error del servidor"); 
            }
            //Como el Task pide retornar un string, retornamos el username del usuario, devolviendo tambien un mensaje. 
            _logger.LogInformation($"Usuario eliminado exitosamente {existeUser.Username}");
            return existeUser.Username!; 

        }
    }
}
