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
using BETSoftware.Domain.Models.Dtos;

namespace BETSoftware.UnitTests
{
    [TestClass]
    public class OrdersServiceTests
    {
        private ILoginService _loginService;
        private IOrderService _ordersService;
        private IProductService _productsService;
        private IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            var services = new ServiceCollection();

            #region Mapper Configuration
            var mockMapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new OrderProfile());
                configuration.AddProfile(new ProductProfile());
                configuration.AddProfile(new LoginProfile());
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
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddLogging();
            #endregion

            var sp = services.BuildServiceProvider();

            _ordersService = sp.GetRequiredService<IOrderService>();
            _productsService = sp.GetRequiredService<IProductService>();
            _loginService = sp.GetRequiredService<ILoginService>();
        }

        [TestMethod]
        public async Task Can_Create_Order_Record_Async()
        {
            var user = GetCurrentUser();
            var product = GetCurrentProduct();
            var orderDetail = new OrderInDto
            {
                Active = true,
                CustomerId = user.Id,
                ShippingAddress = "10 Maliza Place, Mbuqe Extension, Mthatha, 5099",
                ShippingRequired = true,
                ShippingTax = 14.00M,
                TotalAmount = 245.00M,
            };
            var order = _mapper.Map<Order>(orderDetail);
            order.Date = DateTime.UtcNow;
            var insertedOrder = await _ordersService.Insert(order);

            Assert.IsNotNull(insertedOrder);
            Assert.IsNotNull(insertedOrder);
            Assert.IsTrue(insertedOrder.Active == true);
            Assert.IsTrue(insertedOrder.CustomerId == user.Id);

            var createLines = CreateOrderLines(product.Result, insertedOrder);
            insertedOrder.Lines = createLines;

            var editedOrder = await _ordersService.Update(insertedOrder);

            Assert.IsNotNull(editedOrder);
            Assert.IsTrue(editedOrder.Active == true);
            Assert.IsTrue(editedOrder.Lines.Count == 3);

            var orders = await _ordersService.GetAll();

            Assert.IsNotNull(orders);
            Assert.AreEqual(1, orders.Count);
            Assert.IsTrue(orders.First().Lines.Count == 3);
            Assert.IsTrue(orders.First().Active == true);
        }

        [TestMethod]
        public async Task Can_Delete_Order_Async()
        {
            var user = GetCurrentUser();
            var product = GetCurrentProduct().Result;
            var orderDetail = new OrderInDto
            {
                Active = true,
                CustomerId = user.Id,
                ShippingAddress = "10 Maliza Place, Mbuqe Extension, Mthatha, 5099",
                ShippingRequired = true,
                ShippingTax = 14.00M,
                TotalAmount = 245.00M,
            };
            var order = _mapper.Map<Order>(orderDetail);
            order.Date = DateTime.UtcNow;
            var insertedOrder = await _ordersService.Insert(order);

            Assert.IsNotNull(insertedOrder);
            Assert.IsNotNull(insertedOrder);
            Assert.IsTrue(insertedOrder.Active == true);
            Assert.IsTrue(insertedOrder.CustomerId == user.Id);

            var createLines = CreateOrderLines(product, insertedOrder);
            insertedOrder.Lines = createLines;

            var editedOrder = await _ordersService.Update(insertedOrder);

            Assert.IsNotNull(editedOrder);
            Assert.IsTrue(editedOrder.Active == true);
            Assert.IsTrue(editedOrder.Lines.Count == 3);

            var deleteOrder = await _productsService.Delete(insertedOrder.Id);

            Assert.IsNotNull(deleteOrder);
            Assert.IsTrue(deleteOrder.Active == false);
        }

        private static List<OrderLines> CreateOrderLines(Product product, Order order)
        {
            var line = new OrderLines
            {
                ProductId = product.Id,
                OrderId = order.Id,
                Qty = 3
            };
            order.Lines.Add(line);

            line = new OrderLines
            {
                ProductId = product.Id,
                OrderId = order.Id,
                Qty = 5
            };
            order.Lines.Add(line);

            line = new OrderLines
            {
                ProductId = product.Id,
                OrderId = order.Id,
                Qty = 1
            };
            order.Lines.Add(line);

            return order.Lines;
        }

        private async Task<Product> GetCurrentProduct()
        {
            var productDetail = new ProductInDto
            {
                Active = true,
                Description = "Product X Description",
                ImageUri = "/images/productx.png",
                Name = "Product X",
                Price = 205.90M
            };
            var product = _mapper.Map<Product>(productDetail);
            var insertedProduct = await _productsService.Insert(product);

            return insertedProduct;
        }

        private async Task<Login> GetCurrentUser()
        {
            var loginDetail = new LoginInDto
            {
                EmailAddress = "user@somedomain.com",
                Password = "password@12321",
                Username = "mtyide"
            };
            var login = _mapper.Map<Login>(loginDetail);
            await _loginService.Create(login);
            var user = await _loginService.GetLogin(login);

            return user;
        }
    }
}
