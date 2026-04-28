

using AutoMapper;
using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Application.Exceptions;

using MediatR;
using Microsoft.Extensions.Logging;

namespace canchasfutbol.Application.Features.Reservas.Commands.Update
{
    public class UpdateReservaCommandHandler : IRequestHandler<UpdateRerservaCommand, Unit>
    {
       
        private readonly ILogger<UpdateReservaCommandHandler> _logger;
        private readonly IUnitOfWorkRepository _unitOfWork;

        public UpdateReservaCommandHandler(ILogger<UpdateReservaCommandHandler> logger, IUnitOfWorkRepository unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            
        }


        public async Task<Unit> Handle(UpdateRerservaCommand request, CancellationToken cancellationToken)
        {
            
            // Buscar que el ID de la reserva exista
            var reserva = await _unitOfWork.ReservaRepository.GetByGuidAsync(request.IdReserva);
            if (reserva == null)
            {
                _logger.LogError("No existe la reserva con ese ID");
                throw new NotFoundException("No existe la reserva con ese ID");
                
            }

            // Verificar que no exista una reserva en el nuevo horario seleccionado. 
            var existe = await _unitOfWork.ReservaRepository.ExistsOverlap(
                reserva.CanchaId,
                request.Day,
                request.StartHour,
                request.EndHour); 

            if (existe)
                throw new Exception("La cancha ya está reservada para ese horario");

            //Buscar cancha para obtener el precio por hora
            var cancha = await _unitOfWork.CanchasRepository.GetByGuidAsync(request.IdCancha);

            //Modificar la reserva con los nuevos datos
            reserva.Reprogramar(
                request.Day,
                request.StartHour,
                request.EndHour,
                cancha.Preciohora );

           
            // Guardar los cambios
            _unitOfWork.ReservaRepository.UpdateEntity(reserva);
            var result = await _unitOfWork.Complete();
            if(result <= 0)
            {
                _logger.LogError("No se pudo actualizar la reserva");
                throw new Exception("No se pudo actualizar la reserva");
            }
            _logger.LogInformation($"Reserva {reserva.Id} actualizada exitosamente.");

            return Unit.Value; 
        }

    }
}
