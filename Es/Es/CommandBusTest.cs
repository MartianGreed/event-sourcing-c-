using System.Collections.Generic;
using Es.Exception;
using Moq;
using Xunit;

namespace Es
{
    public class CommandBusTest
    {
        private CommandBus _bus;

        public CommandBusTest()
        {
            _bus = new CommandBus();
        }
        
        [Fact]
        public void TestItMustHaveRegisteredHandlersBeforeDispatch()
        {
            Assert.Throws<HandlersNotRegisteredException>(() => _bus.Dispatch(new FakeCommand()));
        }

        [Fact]
        public void TestItProperlyCallHandlers()
        {
            var command = new FakeCommand();
            
            var registryMock = new Mock<ICommandHandlerRegistry>();
            var handlerMock1 = new Mock<ICommandHandler>();
            var handlerMock2 = new Mock<ICommandHandler>();

            registryMock.Setup(r => r.HasHandlers("FakeCommand")).Returns(true);
            registryMock.Setup(r => r.GetHandlers("FakeCommand")).Returns(new List<ICommandHandler>(){ handlerMock1.Object, handlerMock2.Object });

            _bus.SetHandlerRegistry(registryMock.Object);
            _bus.Dispatch(command);
            
            registryMock.Verify(r => r.GetHandlers("FakeCommand"), Times.Once);
            handlerMock1.Verify(h => h.Handle(command), Times.Once);
            handlerMock2.Verify(h => h.Handle(command), Times.Once);
        }
    }
}