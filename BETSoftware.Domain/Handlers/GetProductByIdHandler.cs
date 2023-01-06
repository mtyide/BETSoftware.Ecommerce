using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Queries;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductService _service;
        public GetProductByIdHandler(IProductService service)
        {
            _service = service;
        }
        public Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) => _service.Get(request.Id);
    }
}
