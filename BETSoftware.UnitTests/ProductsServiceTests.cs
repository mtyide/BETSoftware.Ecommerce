using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using BETSoftware.Domain.Interfaces;
using Api.Configuration;
using BETSoftware.Data;
using BETSoftware.Domain.Services;
using BETSoftware.Domain.Models;
using BETSoftware.Data.Repositories;

namespace BETSoftware.UnitTests
{
    [TestClass]
    public class ProductsServiceTests
    {
        private IProductService _productsService;
        private IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            var services = new ServiceCollection();

            #region Mapper Configuration
            var mockMapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new ProductProfile());
            });
            _mapper = mockMapperConfig.CreateMapper();
            #endregion
            #region Dependency Injection
            services.AddDbContext<Storage>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                options.ConfigureWarnings(w =>
                {
                    w.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                    w.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning);
                }
                );
            });
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddLogging();
            #endregion

            var sp = services.BuildServiceProvider();

            _productsService = sp.GetRequiredService<IProductService>();
        }

        [TestMethod]
        public async Task Can_Create_Product_Record_Async()
        {
            var productDetail = new Product
            {
                Active = true,
                Description = "Product X Description",
                ImageUri = "/images/productx.png",
                Name = "Product X",
                Price = 205.90M
            };

            var product = _mapper.Map<Product>(productDetail);
            var response = await _productsService.Insert(product);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Name.Equals("Product X"));
            Assert.IsTrue(response.Active == true);

            var products = await _productsService.GetAll();

            Assert.IsNotNull(products);
            Assert.AreEqual(1, products.Count);
            Assert.IsTrue(products.First().Active == true);
        }

        [TestMethod]
        public async Task Can_Update_Product()
        {
            var productDetail = new Product
            {
                Active = true,
                Description = "Product X Description",
                ImageUri = "/images/productx.png",
                Name = "Product X",
                Price = 205.90M
            };

            var product = _mapper.Map<Product>(productDetail);
            var response = await _productsService.Insert(product);
            var edited = response;
            edited.Name = "Product Y";
            edited.Active = false;

            var current = await _productsService.Update(edited);

            Assert.IsNotNull(current);
            Assert.IsNotNull(current);
            Assert.IsTrue(current.Name.Equals("Product Y"));
            Assert.IsTrue(current.Active == false);
        }

        [TestMethod]
        public async Task Can_Get_Active_Products_Async()
        {
            var productDetail = new Product
            {
                Active = true,
                Description = "Product X Description",
                ImageUri = "/images/productx.png",
                Name = "Product X",
                Price = 205.90M
            };

            var product = _mapper.Map<Product>(productDetail);
            await _productsService.Insert(product);

            var products = await _productsService.GetActive();

            Assert.IsNotNull(products);
            Assert.AreEqual(1, products.Count);
            Assert.IsTrue(products.First().Active == true);
        }

        [TestMethod]
        public async Task Can_Get_All_Products_Async()
        {
            var productDetail = new Product
            {
                Active = true,
                Description = "Product X Description",
                ImageUri = "/images/productx.png",
                Name = "Product X",
                Price = 205.90M
            };

            var product = _mapper.Map<Product>(productDetail);
            await _productsService.Insert(product);

            var products = await _productsService.GetAll();

            Assert.IsNotNull(products);
            Assert.AreEqual(1, products.Count);
            Assert.IsTrue(products.First().Active == true);
        }
    }
}