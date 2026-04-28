using canchasfutbol.Application.Features.Reservas.Queries.GetAllReservas;
using MediatR;


namespace canchasfutbol.Application.Features.Reservas.Queries.GetReservaByDay
{
    public class GetRerservaByDayQuery : IRequest<List<ReservaVm>>
    {
        public DateOnly Day { get; set; }


    }
}
