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
using NuGet.Frameworks;
using BETSoftware.Domain.Models.Dtos;

namespace BETSoftware.UnitTests
{
    [TestClass]
    public class LoginServiceTests
    {
        private ILoginService _loginService;
        private IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            var services = new ServiceCollection();

            #region Mapper Configuration
            var mockMapperConfig = new MapperConfiguration(configuration =>
            {
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
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddLogging();
            #endregion

            var sp = services.BuildServiceProvider();

            _loginService = sp.GetRequiredService<ILoginService>();
        }

        [TestMethod]
        public async Task Can_Create_Login_Record_Async()
        {
            var loginDetail = new LoginInDto
            {
                EmailAddress = "user@somedomain.com",
                Password = "password@12321",
                Username = "mtyide"
            };
            var login = _mapper.Map<Login>(loginDetail);
            var response = await _loginService.Create(login);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Username.Equals("mtyide"));
            Assert.IsTrue(response.EmailAddress.Equals("user@somedomain.com"));
        }

        [TestMethod]
        public async Task Can_Login_Async()
        {
            var loginDetail = new LoginInDto
            {
                EmailAddress = "user@somedomain.com",
                Password = "password@12321",
                Username = "mtyide"
            };
            var login = _mapper.Map<Login>(loginDetail);
            var response = await _loginService.Create(login);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Username.Equals("mtyide"));
            Assert.IsTrue(response.EmailAddress.Equals("user@somedomain.com"));

            var success = await _loginService.GetLogin(login);

            Assert.IsNotNull(success);
            Assert.IsTrue(success.Id != 0);
        }
    }
}
