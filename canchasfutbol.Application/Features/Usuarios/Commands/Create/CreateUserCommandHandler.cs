

using AutoMapper;
using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Application.Dtos;
using canchasfutbol.Application.Dtos.Identity;
using canchasfutbol.Application.Helpers;
using canchasfutbol.Application.Utils;
using canchasfutbol.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace canchasfutbol.Application.Features.Usuarios.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseDto<AuthResponse>>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unit;
        private readonly JwtTokenGenerator _jwtToken;
        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IMapper mapper, IUnitOfWorkRepository unit, JwtTokenGenerator jwtToken)
        {
            _logger = logger;
            _mapper = mapper;
            _unit = unit;
            _jwtToken = jwtToken;
        }
        public async Task<ResponseDto<AuthResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Crear instancia de Response 
            var response = new ResponseDto<AuthResponse>();
            try 
            {
                // Validaciones 
                var existeMail = await _unit.UserRepository.GetByEmailAsync(request.Email);
                if (existeMail != null)
                {
                    _logger.LogWarning($"Este mail ya existe: {request.Email}");
                    response.IsSuccess = false;
                    response.Message = "El mail ya existe en la bd";
                    return response;
                }

                var username = await _unit.UserRepository.ExistByUsername(request.Username);
                if (username)
                {
                    _logger.LogWarning($"Este username ya existe: {request.Email}");
                    response.IsSuccess = false;
                    response.Message = "El username ya existe en la bd";
                    return response;
                }

                // Mapear y hashear contrasena
                var nuevoUsuario = _mapper.Map<Usuario>(request);
                nuevoUsuario.Password = PasswordHasher.Hash(request.Password);

                //Guardar en BD
                _unit.UserRepository.AddEntity(nuevoUsuario);
                var result = await _unit.Complete();
                if (result <= 0)
                {
                    _logger.LogError("Error al guardar el usuario en la bd");
                    response.IsSuccess = false;
                    response.Message = "No se pudo guardar el usuario en la bd";
                    return response;
                }
                //Generar Token 

                var token = _jwtToken.GenerateToken(nuevoUsuario.Email, nuevoUsuario.Username, nuevoUsuario.Rol);


                //Devolver respuesta Ok con AuthResponse (nueva instancia) 
                var authResponse = new AuthResponse
                {
                    Email = nuevoUsuario.Email,
                    Username = nuevoUsuario.Username,
                    Token = token
                };

                return ResponseDto<AuthResponse>.Success(authResponse, "Usuario creado exitosamente");


            }

            // Estos catch con mensaje serviran para identificar correctamente el error.
            catch (DbUpdateException Dbex)
            {
                _logger.LogError(Dbex, "Error al guardar en la BD");
                 return ResponseDto<AuthResponse>.Failure(new List<string> { Dbex.Message }, "Error de bd");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado");
                return ResponseDto<AuthResponse>.Failure(new List<string> { ex.Message }, "Error inesperado");
            }
          

            
        }
    }
}
