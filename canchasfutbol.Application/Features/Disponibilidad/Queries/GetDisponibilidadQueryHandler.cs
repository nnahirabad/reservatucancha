using AutoMapper;
using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Application.Dtos;
using canchasfutbol.Application.Exceptions;
using canchasfutbol.Domain.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Disponibilidad.Queries
{
    public class GetDisponibilidadQueryHandler : IRequestHandler<GetDisponibilidadQuery, List<SlotDisponibleDto>>
    {
        private readonly IMapper _mapper; 
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IReservaRepository _reservaRepository; 


        public GetDisponibilidadQueryHandler(IMapper mapper, IUnitOfWorkRepository unit, IReservaRepository reservaRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unit;
            _reservaRepository = reservaRepository;
        }

        private List<SlotDisponibleDto> CalcularDisponibilidad(
              List<Reserva> reservas,
              TimeOnly comienzo,
              TimeOnly final)
        {
            var disponibles = new List<SlotDisponibleDto>();
            var inicioActual = comienzo;

            foreach (var reserva in reservas)
            {
                if (inicioActual < reserva.HoraInicio)
                {
                    disponibles.Add(new SlotDisponibleDto
                    {
                        Inicio = inicioActual,
                        Fin = reserva.HoraInicio
                    });
                }
                if (reserva.HoraFin > inicioActual)
                {
                    inicioActual = reserva.HoraFin;
                }

            }

            if (inicioActual < final)
            {
                disponibles.Add(new SlotDisponibleDto
                {
                    Inicio = inicioActual,
                    Fin = final
                });
            }

            return disponibles;


        }
        public async Task<List<SlotDisponibleDto>> Handle(GetDisponibilidadQuery request, CancellationToken cancellationToken)
        {
           

            // Obtener las reservas para la cancha y fecha especificada
            var reservas = await _reservaRepository.GetReservasByCanchaAndDate(request.CanchaId, request.Fecha); 


            var apertura = new TimeOnly(16, 0); // Hora de apertura (8:00 AM)
            var cierre = new TimeOnly(24, 0); // Hora de cierre (10:00 PM)

            return CalcularDisponibilidad(reservas, apertura , cierre);




        }


    }
}
