

using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace canchasfutbol.Application.Features.Reservas.Commands.Update
{
    public class UpdateRerservaCommand : IRequest<Unit>
    {
          public Guid IdReserva { get; set; }

          

        public UpdateRerservaCommand(Guid id)
        {
            IdReserva = id;
        }

       
        public Guid IdCancha { get; set; }

        public DateOnly Day { get; set; }

        public TimeOnly StartHour { get; set; }

        public TimeOnly EndHour { get; set; }

        
    }
}
