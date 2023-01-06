using BETSoftware.Domain.Commands;
using BETSoftware.Domain.Interfaces;
using BETSoftware.Domain.Models;
using BETSoftware.Domain.Queries;
using MediatR;

namespace BETSoftware.Domain.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductService _service;
        public UpdateProductHandler(IProductService service)
        {
            _service = service;
        }
        public Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken) => _service.Update(request.Product);
    }
}
