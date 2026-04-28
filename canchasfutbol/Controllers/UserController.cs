using canchasfutbol.Application.Dtos;
using canchasfutbol.Application.Dtos.Identity;
using canchasfutbol.Application.Features.Usuarios.Commands.Create;
using canchasfutbol.Application.Features.Usuarios.Commands.Delete;
using canchasfutbol.Application.Features.Usuarios.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace canchasfutbol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost("Register")]
        
        public async Task<ActionResult<ResponseDto<AuthResponse>>> RegisterUser([FromBody] CreateUserCommand query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("Login")]
        
        public async Task<ActionResult<ResponseDto<AuthResponse>>> Login([FromBody] LoginQuery query)
        {
            return await _mediator.Send(query);
        }



        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete([FromQuery] Guid id)
        {
            var command = new DeleteUserCommand(id);
            return await _mediator.Send(command);

        }
    }
}
