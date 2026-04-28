

using AutoMapper;
using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Application.Exceptions;
using canchasfutbol.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace canchasfutbol.Application.Features.Reservas.Commands.Create
{
    public class CreateReservaCommandHandler : IRequestHandler<CreateRerservaCommand, Guid>
    {
        private readonly ILogger<CreateReservaCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unit; 

        public CreateReservaCommandHandler(ILogger<CreateReservaCommandHandler> logger, IMapper mapper, IUnitOfWorkRepository unit)
        {
            _logger = logger;
            _mapper = mapper;
            _unit = unit;
        }

        public async Task<Guid> Handle(CreateRerservaCommand request, CancellationToken cancellationToken)
        {


            try
            {

                // Validar que la cancha exista. 


                var cancha = await _unit.CanchasRepository.GetByGuidAsync(request.CanchaId);
                if (cancha == null)
                {
                    _logger.LogError($"Cancha con id {request.CanchaId} no existe.");
                    throw new Exception($"Cancha with id {request.CanchaId} not found.");
                }

                // Calcular hora fin 
                var horaFin = request.HoraInicio.Value.AddHours(request.CantHoras);
                // Calcular costo
                var costo = cancha.Preciohora * request.CantHoras;

                // VERIFICAR QUE NO HAYA SOLAPAMIENTO DE RESERVAS PARA LA CANCHA EN LA FECHA Y HORA SOLICITADA

                var haySolapamiento = await _unit.ReservaRepository.ExistsOverlap(request.CanchaId, request.Fecha, request.HoraInicio, horaFin);

                if (haySolapamiento)
                    throw new BusinessException("La cancha ya está reservada para la fecha y hora solicitada.");

                // CREAR RESERVA 

                var reserva = Reserva.Create(
                    request.UserId,
                    request.CanchaId,
                    request.Fecha,
                    request.HoraInicio.Value,
                    horaFin,
                    cancha.Preciohora
                    );

                await _unit.ReservaRepository.AddAsync(reserva);
                await _unit.Complete();
                _logger.LogInformation($"Reserva {reserva.Id} creada exitosamente.");

                return reserva.Id;

            }
            catch (DbUpdateException)
            {
                throw new Exception("Ese horario acaba de ser reservado por otro usuario.");
            }
        }
    }
}
