using AutoMapper;
using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Application.Features.Reservas.Queries.GetAllReservas;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Reservas.Queries.GetReservaByUser
{
    public class GetReservaByUserHandler : IRequestHandler<GetReservaByUserQuery, List<ReservaVm>>
    {
        private readonly IMapper _mapper; 
        private readonly ILogger<GetReservaByUserHandler> _logger;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;   

        public GetReservaByUserHandler(IMapper mapper, ILogger<GetReservaByUserHandler> logger, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        public async Task<List<ReservaVm>> Handle(GetReservaByUserQuery request, CancellationToken cancellationToken)
        {
            var existUser = await _unitOfWorkRepository.UserRepository.GetByName(request.User);
            if (existUser == null) {
                _logger.LogWarning("No existe el usuario"); 
            }
            var reserva = await _unitOfWorkRepository.ReservaRepository.GetReservaByUsername(existUser.Username); 
            if(reserva == null )
            {
                _logger.LogWarning($"No se encontraron reservas para el usuario: {existUser.Username}");
                
            }
            var reservaVm = _mapper.Map<List<ReservaVm>>(reserva);
            return reservaVm;
        }
    }
}
