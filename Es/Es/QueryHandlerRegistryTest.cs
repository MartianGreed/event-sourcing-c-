using System;
using System.Collections.Generic;
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
            _registry.AddHandler(new FakeQueryHandler());
            Assert.Equal(1, _registry.Size());
        }
        
        [Fact]
        public void TestItRegistersTheHandlerWithTheRightKey()
        {
            var handler = new FakeQueryHandler();
            _registry.AddHandler(handler);
            Assert.Same(handler, _registry.GetHandler("FakeQuery"));
        }
        
        [Fact]
        public void TestItThrowsAnExceptionIfNoHandlerFound()
        {
            Assert.Throws<NoQueryHandlerRegisterForQuery>(() => _registry.GetHandler("AnotherFakeQuery"));
        }
        
        [Fact]
        public void TestHasHandlerChecksOnKey()
        {
            var handler = new FakeQueryHandler();
            _registry.AddHandler(handler);
            
            Assert.False(_registry.HasHandler("FakeQueryHandler"));
            Assert.True(_registry.HasHandler("FakeQuery"));
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