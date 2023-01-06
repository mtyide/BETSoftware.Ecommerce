using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Queries;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly IProductService _service;

        public GetProductsHandler(IProductService service)
        {
            _service = service;
        }

        public Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken) => _service.GetAll();
    }
}
