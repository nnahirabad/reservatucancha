using canchasfutbol.Application.Dtos;
using MediatR;


namespace canchasfutbol.Application.Features.Reservas.Queries.GetAllReservas
{
    public class GetAllReservasQuery : IRequest<List<ReservaVm>>
    {
    }
}
