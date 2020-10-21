using System;
using System.Collections.Generic;
using Es.Exception;
using Xunit;

namespace Es.Domain
{
    public class DomainEventStreamTest
    {
        [Fact]
        public void TestItCanAppendToStream()
        {
            DomainEventStream _stream = GenerateSimpleDomainEventStream();

            Assert.Equal(3, _stream.Size());
            var append = DomainEventStream.FromKeyValuePair(new Dictionary<int, DomainMessage>()
            {
                {3, new DomainMessage(Identity<InternalIdentity>.Generate().ToString(), 3, new TestEvent("Dummy payload"), DateTimeImmutable.Now())}
            });
            _stream.AppendStream(append);
            Assert.Equal(4, _stream.Size());
        }
        
        [Fact]
        public void TestItThrowsExceptionOnDuplicatedPlayhead()
        {
            DomainEventStream _stream = GenerateSimpleDomainEventStream();

            Assert.Equal(3, _stream.Size());
            var append = DomainEventStream.FromKeyValuePair(new Dictionary<int, DomainMessage>()
            {
                {2, new DomainMessage(Identity<InternalIdentity>.Generate().ToString(), 2, new TestEvent("Dummy payload"), DateTimeImmutable.Now())}
            });
            Assert.Throws<DuplicatedPlayheadException>(() => _stream.AppendStream(append));
        }
        
        [Fact]
        public void TestItCanBeIterratedOver()
        {
            DomainEventStream _stream = GenerateSimpleDomainEventStream();

            foreach (var domainMessage in _stream)
            {
                Assert.Equal(domainMessage.Key, domainMessage.Value.Playhead);
            }
        }
        
        private DomainEventStream GenerateSimpleDomainEventStream()
        {
            return DomainEventStream.FromKeyValuePair(new Dictionary<int, DomainMessage>()
            {
                {0, new DomainMessage(Identity<InternalIdentity>.Generate().ToString(), 0, new TestEvent("This is a dumb payload"), DateTimeImmutable.Now())},
                {1, new DomainMessage(Identity<InternalIdentity>.Generate().ToString(), 1, new TestEvent("This is another dumb payload"), DateTimeImmutable.Now())},
                {2, new DomainMessage(Identity<InternalIdentity>.Generate().ToString(), 2, new TestEvent("This is another dumbshit payload"), DateTimeImmutable.Now())},
            });
        }
    }

    class TestEvent : IEvent
    {
        public string Payload { get; }

        public TestEvent(string payload)
        {
            Payload = payload;
        }
    }
}