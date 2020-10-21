using System;
using System.Collections.Generic;
using Es.Exception;
using Xunit;

namespace Es
{
    public class CommandHandlerRegistryTest
    {
        private CommandHandlerRegistry _registry;

        public CommandHandlerRegistryTest()
        {
            _registry = new CommandHandlerRegistry(new List<ICommandHandler>());    
        }
        
        [Fact]
        public void TestItCanAddAHandler()
        {
            _registry.AddHandler("CommandToHandle", new YetAnotherCommandHandler());
            Assert.True(_registry.HasHandlers("CommandToHandle"));
            Assert.Equal(1, _registry.Size());
        }

        [Fact]
        public void TestItCanRegisterMultipleCommandHandlerToHandleSingleCommand()
        {
            _registry.AddHandler("CommandToHandle", new YetAnotherCommandHandler());
            _registry.AddHandler("CommandToHandle", new AnotherCommandHandler());
            Assert.Equal(2, _registry.GetHandlers("CommandToHandle").Count);
        }
        
        [Fact]
        public void TestItThrowsAnExceptionIfNoHandlersAreRegistered()
        {
            Assert.Throws<NoCommandyHandlerRegisterForCommand>(() => _registry.GetHandlers("NotRegisteredCommand"));
        }
    }

    class AnotherCommandHandler : ICommandHandler
    {
        public void Handle(ICommand command)
        {
            Console.WriteLine(command);
        }

        public string Supports()
        {
            return "CommandToHandle";
        }
    }

    class YetAnotherCommandHandler : ICommandHandler
    {
        public void Handle(ICommand command)
        {
            Console.WriteLine(command);
        }

        public string Supports()
        {
            return "CommandToHandle";
        }
    }
}