using AutoMapper;
using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Models.Dtos;
using BETSoftware.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [Authorize]
    [Route("users")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;
        private readonly IConfiguration _configuration;
        public LoginController(IMediator mediator, IMapper mapper, ILogger<OrdersController> logger, IConfiguration configuration)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("insertUser")]
        public async Task<IActionResult> PostLogin([FromBody] LoginInDto loginDto)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var login = _mapper.Map<Login>(loginDto);
            var result = await _mediator.Send(new InsertLoginCommand(login));

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post([FromBody] LoginInDto loginDto)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var login = _mapper.Map<Login>(loginDto);
            var result = await _mediator.Send(new GetLoginQuery(login));

            if (result == null) { return NotFound(); }

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtIssuerOptions")["Key"]);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, result.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handler.CreateToken(descriptor);

            if (token == null) { return Unauthorized(); }

            var success = new LoginOutDto { Token = handler.WriteToken(token), Expires = token.ValidTo, Id = result.Id };

            return Ok(success);
        }
    }
}
