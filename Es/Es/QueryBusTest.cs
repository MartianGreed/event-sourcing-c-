using System;
using System.Collections.Generic;
using Es.Exception;
using Moq;
using Xunit;

namespace Es
{
    public class QueryBusTest
    {
        [Fact]
        public void TestItMustHaveHandlers()
        {
            QueryBus bus = new QueryBus();
            
            Assert.Throws<HandlersNotRegisteredException>(() => bus.Ask(new TestQuery()));
        }
        
        [Fact]
        public void TestItCanAskDataStore()
        {
            var registryMock = new Mock<IQueryHandlerRegistry>();
            registryMock.Setup(r => r.GetHandler("TestQuery")).Returns(new TestQueryHandler());
            
            QueryBus bus = new QueryBus();
            bus.SetHandlerRegistry(registryMock.Object);
            
            IQueryResult res = bus.Ask(new TestQuery());
            var result = (QueryResult) res;
            Assert.Equal("Test", result.Data);
        }
        
    }
    
    public class TestQuery : IQuery
    {
    }
    public class TestQueryHandler : IQueryHandler
    {
        public QueryResult Handle(IQuery query)
        {
            var q = (TestQuery) query;
            return new QueryResult("Test");
        }
    }
}