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

        [HttpPost]
        [Route("getProducts")]
        public async Task<List<Product>?> GetProducts(Filter filter)
        {
            if (!ModelState.IsValid) { return null; }

            var result = await _mediator.Send(new GetProductsQuery());
            if (result == null) { return null; }
            if (filter.Page < 1) { filter.Page = 1; }
            if (filter.Size < 1) { filter.Size = 50; }
            if (filter.Page == 1) { return result.Take(filter.Size).ToList(); }

            var recordsPerPage = (filter.Page - 1) * 50;
            return result.Skip(recordsPerPage).Take(filter.Size).ToList();
        }

        [HttpGet]
        [Route("getProductById/{id}")]
        public async Task<Product> GetProductById(int id) => await _mediator.Send(new GetProductByIdQuery(id));

        [HttpPost]
        [Route("insertProduct")]
        public async Task<Product?> PostProduct([FromBody] ProductInDto productDto)
        {
            if (!ModelState.IsValid) { return null; }

            var product = _mapper.Map<Product>(productDto);
            return await _mediator.Send(new InsertProductCommand(product));
        }

        [HttpPut]
        [Route("updateProduct/{id}")]
        public async Task<Product?> Put(int id, [FromBody] ProductInDto productDto)
        {
            if (!ModelState.IsValid) { return null; }

            var model = await _mediator.Send(new GetProductByIdQuery(id));

            if (model == null) { return null; }

            var product = _mapper.Map<Product>(productDto);
            product.Id = id;
            return await _mediator.Send(new UpdateProductCommand(product)); 
        }

        [HttpDelete]
        [Route("deleteProduct/{id}")]
        public async Task<Product?> Delete(int id)
        {
            if (!ModelState.IsValid) { return null; }

            var product = await _mediator.Send(new GetProductByIdQuery(id));

            if (product == null) { return null; }

            product.Id = id;
            return await _mediator.Send(new DeleteProductCommand(product));
        }
    }
}
