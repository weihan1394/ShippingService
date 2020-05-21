using ShippingService.Api.Controllers;
using Moq.AutoMock;

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

            Controller = Mocker.CreateInstance<T>();
        }
    }
}
