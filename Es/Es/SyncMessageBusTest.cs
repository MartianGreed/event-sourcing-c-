using System;
using System.Collections.Generic;
using Es.Exception;
using Xunit;

namespace Es
{
    public class FakeCommand : ICommand
    {
        public string Name;
    }
    
    public class FakeCommandHandler : ICommandHandler
    {
        public  void Handle(ICommand command)
        {
            FakeCommand cmd = (FakeCommand) command;
            
            Console.WriteLine(cmd.Name);
        }
    }
    
    public class SyncMessageBusTest
    {
        private readonly SyncMessageBus _bus;

        public SyncMessageBusTest()
        {
            this._bus = new SyncMessageBus(new Dictionary<string, ICommandHandler>()
            {
                {"FakeCommandHandler", new FakeCommandHandler()},
            });
        }

        [Fact]
        public void TestHandleWorks()
        {
            FakeCommand cmd = new FakeCommand();
            cmd.Name = "Test";
            
            this._bus.Handle(cmd);
        }

        [Fact]
        public void TestItThrowsAnExceptionIfNoHandlerFound()
        {
            SyncMessageBus bus = new SyncMessageBus(new Dictionary<string, ICommandHandler>() {});
            Assert.Throws<NoHandlerFoundForCommand>(() => bus.Handle(new FakeCommand()));
        }
    }
}