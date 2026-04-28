using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Application.Dtos;
using canchasfutbol.Application.Dtos.Identity;
using canchasfutbol.Application.Helpers;
using canchasfutbol.Application.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Usuarios.Queries.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, ResponseDto<AuthResponse>>
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly ILogger<LoginHandler> _logger;
        private readonly JwtTokenGenerator _generatorToken;

        public LoginHandler(IUnitOfWorkRepository unitOfWorkRepository, ILogger<LoginHandler> logger, JwtTokenGenerator generatorToken)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _logger = logger;
            _generatorToken = generatorToken;
        }



        public async Task<ResponseDto<AuthResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<AuthResponse>();
            try
            {
                var usuario = await _unitOfWorkRepository.UserRepository.GetByEmailAsync(request.Email);
                if (usuario != null) { 
                  _logger.LogWarning($"No existe usuario registrado con ese mail: {request.Email}");
                    throw new Exception("Datos incorrectos"); 

                }
                var contrasena = PasswordHasher.Verify(request.Password, usuario.Password); 
                if(contrasena.Equals(false))
                {
                    _logger.LogInformation("La contrasena no coincide con el mail registrado");
                    throw new Exception("Contrasena incorrecta"); 

                }

                var token = _generatorToken.GenerateToken(usuario.Email, usuario.Username!, usuario.Rol!);

                var authResponse = new AuthResponse
                {
                    Token = token,
                    Username = usuario.Username!,
                    Email = usuario.Email,
                    Rol = usuario.Rol!
                };

                return ResponseDto<AuthResponse>.Success(authResponse, "Inicio de sesion exitoso");

            }
            catch (DbUpdateException dex) { 
            
                _logger.LogError(dex, "Error al acceder a la base de datos durante el inicio de sesion");
                return ResponseDto<AuthResponse>.Failure(new List<string> { dex.InnerException?.Message ?? dex.Message}, "Error al guardar en BD");

            }
            catch (Exception ex)
            {
            
                _logger.LogError(ex, "Error inesperado durante el inicio de sesion");
                return ResponseDto<AuthResponse>.Failure( new List<string> { ex.Message }, "Error inesperado");


            }

        }
    }
}
