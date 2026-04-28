using AutoMapper;
using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Reservas.Queries.GetAllReservas
{
    public class GetAllReservasQueryHandler : IRequestHandler<GetAllReservasQuery, List<ReservaVm>>
    {

        private readonly ILogger<GetAllReservasQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository unitOfWorkRepository; 

        public GetAllReservasQueryHandler(ILogger<GetAllReservasQueryHandler> logger, IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _logger = logger;
            _mapper = mapper;
            this.unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<List<ReservaVm>> Handle(GetAllReservasQuery request, CancellationToken cancellationToken)
        {
            var allReservas = await unitOfWorkRepository.ReservaRepository.GetAllAsync(); 
            return _mapper.Map<List<ReservaVm>>(allReservas);
        }
    }
}
