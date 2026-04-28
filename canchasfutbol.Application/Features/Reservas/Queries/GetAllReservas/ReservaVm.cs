using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Reservas.Queries.GetAllReservas
{
    public class ReservaVm 
    {
        public Guid Id { get; set; }
        public string Username  { get; set; }

        public string NombreCancha { get; set; }

        public decimal Costo { get; set; }

        public DateOnly Fecha { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFin { get; set; }


    }
}
