using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Queries;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetActiveProductsQuery, List<Product>>
    {
        private readonly IProductService _service;

        public GetProductsHandler(IProductService service)
        {
            _service = service;
        }

        public Task<List<Product>> Handle(GetActiveProductsQuery request, CancellationToken cancellationToken) => _service.GetAll();
    }
}
