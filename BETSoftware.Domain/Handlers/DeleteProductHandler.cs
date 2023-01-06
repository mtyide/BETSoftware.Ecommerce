using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Product>
    {
        private readonly IProductService _service;
        public DeleteProductHandler(IProductService service)
        {
            _service = service;
        }
        public Task<Product> Handle(DeleteProductCommand request, CancellationToken cancellationToken) => _service.Delete(request.Product);
    }
}
