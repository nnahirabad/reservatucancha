

using AutoMapper;
using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Application.Exceptions;
using canchasfutbol.Application.Features.Reservas.Queries.GetAllReservas;
using MediatR;
using Microsoft.Extensions.Logging;

namespace canchasfutbol.Application.Features.Reservas.Queries.GetReservaByDay
{
    public class GetRerservaByDayHandler : IRequestHandler<GetRerservaByDayQuery, List<ReservaVm>>
    {

        private readonly ILogger<GetRerservaByDayHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWork; 

        public GetRerservaByDayHandler(ILogger<GetRerservaByDayHandler> logger, IMapper mapper, IUnitOfWorkRepository unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<List<ReservaVm>> Handle(GetRerservaByDayQuery request, CancellationToken cancellationToken)
        {
            var reservas = await _unitOfWork.ReservaRepository.GetReservaByDay(request.Day);
            if (reservas == null) {

                throw new NotFoundException();
            }

            var reservasVm = _mapper.Map<List<ReservaVm>>(reservas);
            return reservasVm;
        }
    }
}
