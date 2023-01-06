using AutoMapper;
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
        [Route("login")]
        public async Task<LoginOutDto?> Post([FromBody] LoginInDto loginDto)
        {
            if (!ModelState.IsValid) { return null; }

            var login = _mapper.Map<Login>(loginDto);
            var result = await _mediator.Send(new GetLoginQuery(login));

            if (result == null) { return null; }

            var token = new LoginOutDto { Token = Guid.NewGuid().ToString() };

            return token;
        }
    }
}
