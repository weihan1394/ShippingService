using ShippingService.Api.Controllers;
using Moq.AutoMock;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ShippingService.Api.UnitTests.Controllers
{
    public abstract class ControllerTestsBase<T>
        where T : ApiControllerBase
    {
        protected readonly T Controller;
        protected readonly AutoMocker Mocker;

        protected ControllerTestsBase()
        {
            Mocker = new AutoMocker();

            var httpResponseMock = Mocker.GetMock<HttpResponse>();
            httpResponseMock.Setup(mock => mock.Headers).Returns(new HeaderDictionary());

            var httpRequestMock = Mocker.GetMock<HttpRequest>();

            var httpContextMock = Mocker.GetMock<HttpContext>();
            httpContextMock.Setup(mock => mock.Response).Returns(httpResponseMock.Object);
            httpContextMock.Setup(mock => mock.Request).Returns(httpRequestMock.Object);

            //Mock
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json");
            IConfiguration config = builder.Build();

            Mocker.Use<IConfiguration>(config);

            Controller = Mocker.CreateInstance<T>();
            Controller.ControllerContext.HttpContext = httpContextMock.Object;
            
        }
    }
}
