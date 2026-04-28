using canchasfutbol.Application.Features.Canchas;
using canchasfutbol.Application.Features.Disponibilidad.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace canchasfutbol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanchaController : ControllerBase
    {

        private readonly CanchaService _canchaService;
        private IMediator _mediator; 
       

        public CanchaController(CanchaService canchaService, IMediator mediator)
        {
                _canchaService = canchaService;
                _mediator = mediator;



        }

        // GET: api/<CanchaController>
        [HttpGet("GetCanchas")]
        public async Task<IActionResult> Get()
        {
            var canchas = await  _canchaService.GetAllAsync();
            
            return Ok(canchas);

        }

        // GET api/<CanchaController>/5
        [HttpGet("{canchaid}/disponibilidad")]
        public async Task<IActionResult> GetDisponibilidad(Guid canchaid,[FromQuery] DateOnly fecha)
        {

            var result = await _mediator.Send(
                new GetDisponibilidadQuery(canchaid, fecha)); 

            return Ok(result);
        }

        // POST api/<CanchaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CanchaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            var cancha = await _canchaService.GetByIdAsync(id);
            if (cancha == null) return NotFound();
            cancha.Name = value; // Aquí podrías actualizar otras propiedades según sea necesario
            var actualizado = await _canchaService.UpdateAsync(cancha);
            if (!actualizado) return BadRequest();
            return NoContent();
        }

        // DELETE api/<CanchaController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
