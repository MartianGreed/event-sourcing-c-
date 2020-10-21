using System;
using System.Collections.Generic;
using System.Linq;
using Es.Exception;
using Xunit;

namespace Es
{
    public class QueryHandlerRegistryTest
    {
        private QueryHandlerRegistry _registry;

        public QueryHandlerRegistryTest()
        {
            _registry = new QueryHandlerRegistry(new List<IQueryHandler>());
        }

        [Fact]
        public void TestItCanAddHandler()
        {
            _registry.AddHandler("FakeQuery", new FakeQueryHandler());
            Assert.Equal(1, _registry.Size());
        }
        
        [Fact]
        public void TestItRegistersTheHandlerWithTheRightKey()
        {
            var handler = new FakeQueryHandler();
            _registry.AddHandler("FakeQuery", handler);
            Assert.Same(handler, _registry.GetHandlers("FakeQuery").First());
        }
        
        [Fact]
        public void TestItThrowsAnExceptionIfNoHandlerFound()
        {
            Assert.Throws<NoQueryHandlerRegisterForQuery>(() => _registry.GetHandlers("AnotherFakeQuery"));
        }
        
        [Fact]
        public void TestHasHandlerChecksOnKey()
        {
            var handler = new FakeQueryHandler();
            _registry.AddHandler("FakeQuery", handler);
            
            Assert.False(_registry.HasHandlers("FakeQueryHandler"));
            Assert.True(_registry.HasHandlers("FakeQuery"));
        }
    }

    public class FakeQueryHandler : IQueryHandler
    {
        public QueryResult Handle(IQuery query)
        {
            Console.WriteLine(query);
            return new QueryResult("FakeQueryResult");
        }
    }
}