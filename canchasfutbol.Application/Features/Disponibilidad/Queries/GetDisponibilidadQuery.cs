using canchasfutbol.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Disponibilidad.Queries
{
    public class GetDisponibilidadQuery(Guid CanchaId, DateOnly Fecha) : IRequest<List<SlotDisponibleDto>>
    {
        public Guid CanchaId { get; } = CanchaId;
        public DateOnly Fecha { get; } = Fecha;
    }
}
