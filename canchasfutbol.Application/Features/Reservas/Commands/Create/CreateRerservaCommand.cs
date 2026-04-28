

using MediatR;

namespace canchasfutbol.Application.Features.Reservas.Commands.Create
{
    public class CreateRerservaCommand : IRequest<Guid>
    {

        // Clase que recibe todos los parametros que envia el cliente
        public Guid UserId { get; set; } 

        public Guid CanchaId { get; set; }

        public DateOnly Fecha { get; set; }

        public TimeOnly? HoraInicio { get; set; }

        public int CantHoras { get; set;  } 
        }
}
