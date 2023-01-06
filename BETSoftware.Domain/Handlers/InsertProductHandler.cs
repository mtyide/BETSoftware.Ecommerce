using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class InsertProductHandler : IRequestHandler<InsertProductCommand, Product>
    {
        private readonly IProductService _service;
        public InsertProductHandler(IProductService service)
        {
            _service = service;
        }

        public Task<Product> Handle(InsertProductCommand request, CancellationToken cancellationToken) => _service.Insert(request.Product);
    }
}
