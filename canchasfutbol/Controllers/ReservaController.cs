using canchasfutbol.Application.Features.Reservas.Commands.Create;
using canchasfutbol.Application.Features.Reservas.Queries.GetAllReservas;
using canchasfutbol.Application.Features.Reservas.Queries.GetReservaByUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace canchasfutbol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        private IMediator _mediator;
        public ReservaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ReservaController>
        [HttpGet("GetReserva")]
        public async Task<ActionResult<List<ReservaVm>>> GeAllReservas()
        {
            var query = new GetAllReservasQuery(); 
            var result = await _mediator.Send(query);
            return Ok(result);

        }

        // GET api/<ReservaController>/user
        [HttpGet("by-user")]
        public async Task<IActionResult> GetByUser([FromQuery] string user)
        {
            var reserva = new GetReservaByUserQuery(user); 
            return Ok(await _mediator.Send(reserva));

        }

        // POST api/<ReservaController>
        [HttpPost("CreateReserva")]
        public async Task<IActionResult> Post([FromBody] CreateRerservaCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // PUT api/<ReservaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReservaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
