using AutoMapper;
using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Models.Dtos;
using BETSoftware.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;
        public LoginController(IMediator mediator, IMapper mapper, ILogger<OrdersController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("insertUser")]
        public async Task<IActionResult> PostLogin([FromBody] LoginInDto loginDto)
        {
            if (!ModelState.IsValid) { return NotFound(); }

            var login = _mapper.Map<Login>(loginDto);
            var result = await _mediator.Send(new InsertLoginCommand(login));

            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post([FromBody] LoginInDto loginDto)
        {
            if (!ModelState.IsValid) { return NotFound(); }

            var login = _mapper.Map<Login>(loginDto);
            var result = await _mediator.Send(new GetLoginQuery(login));

            if (result == null) { return NotFound(); }

            var token = new LoginOutDto { Token = Guid.NewGuid().ToString(), Expires = DateTime.UtcNow.AddMinutes(30), Id = result.Id };

            return Ok(token);
        }
    }
}
