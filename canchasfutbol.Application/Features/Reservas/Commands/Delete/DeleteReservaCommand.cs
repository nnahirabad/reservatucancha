using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Reservas.Commands.Delete
{
    public class DeleteReservaCommand : IRequest<Guid>
    {
        public Guid IdReserva { get; set; }
        public DeleteReservaCommand(Guid id)
        {
            IdReserva = id;
        }
    }
}
