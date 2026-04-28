using AutoMapper;
using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Reservas.Commands.Delete
{
    public class DeleteReservaCommandHandler : IRequestHandler<DeleteReservaCommand, Guid>
    {
        private readonly ILogger _logger; 
        private readonly IUnitOfWorkRepository _unitOfWork; 
        private readonly IMapper _mapper;

        public DeleteReservaCommandHandler(ILogger<DeleteReservaCommandHandler> logger, IUnitOfWorkRepository unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(DeleteReservaCommand request, CancellationToken cancellationToken)
        {
            var reservaEntity = _unitOfWork.ReservaRepository.GetByGuidAsync(request.IdReserva); 
            if (reservaEntity == null)
            {
                _logger.LogError($"No existe la reserva con id {request.IdReserva}.");
                throw new Exception($"Reserva with id {request.IdReserva} not found.");
            }

            var reserva = _mapper.Map<Reserva>(reservaEntity);
            _unitOfWork.ReservaRepository.DeleteEntity(reserva);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                throw new Exception("No se pudo eliminar la reserva por error del servidor");
            }
            _logger.LogInformation($"Reserva eliminada exitosamente {reserva.Id}");
            return reserva.Id;
        }
    }
}
