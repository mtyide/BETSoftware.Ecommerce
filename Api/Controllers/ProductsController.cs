using Api.Configuration;
using AutoMapper;
using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Models.Dtos;
using BETSoftware.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IMediator mediator, IMapper mapper, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("getProducts")]
        public async Task<IActionResult> GetProducts()
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var result = await _mediator.Send(new GetActiveProductsQuery());
            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpGet]
        [Route("getProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpGet]
        [Route("getActiveProducts")]
        public async Task<IActionResult> GetActiveProducts()
        {
            var result = await _mediator.Send(new GetActiveProductsQuery());

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpPost]
        [Route("insertProduct")]
        public async Task<IActionResult> PostProduct([FromBody] ProductInDto productDto)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var product = _mapper.Map<Product>(productDto);
            var result = await _mediator.Send(new InsertProductCommand(product));

            return Ok(result);
        }

        [HttpPut]
        [Route("updateProduct/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductInDto productDto)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var product = _mapper.Map<Product>(productDto);
            product.Id = id;
            var result = await _mediator.Send(new UpdateProductCommand(product)); 
            return Ok(result);
        }

        [HttpDelete]
        [Route("deleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var result = await _mediator.Send(new DeleteProductCommand(id));
            return Ok(result);
        }
    }
}
