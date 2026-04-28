using canchasfutbol.Application.Features.Reservas.Queries.GetAllReservas;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Features.Reservas.Queries.GetReservaByUser
{
    public class GetReservaByUserQuery : IRequest<List<ReservaVm>>
    {
        public string User { get; set; }

        public GetReservaByUserQuery(string user)
        {
            User = user; 
        }
    }
}
