using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class EventSourcingServiceCollectionExtensionsTest
    {
        [Fact]
        public void TestItFindAllClassesImplementingInterface()
        {
            var servicesMock = new Mock<IServiceCollection>();

            EventSourcingServiceCollectionExtensions.AddEventSourcingServices(servicesMock.Object);
        }
    }
}