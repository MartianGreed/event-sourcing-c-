using System;
using System.Collections.Generic;
using System.Linq;
using Es.Domain;
using Es.Exception;
using Xunit;

namespace Es.EventStore
{
    public class InMemoryEventStoreTest
    {
        private InMemoryEventStore _store;

        public InMemoryEventStoreTest()
        {
            _store = new InMemoryEventStore();
        }
        
        [Fact]
        public void TestItCanAppendToStore()
        {
            Identity<InternalIdentity> uuid = Identity<InternalIdentity>.Generate();
            DomainEventStream stream = GenerateSimpleDomainEventStream();
            _store.Append(uuid.ToString(), stream);

            DomainEventStream des = _store.Load(uuid.ToString());
            Assert.Equal(3, des.Count());
        }

        [Fact]
        public void TestItThrowsAnExceptionIfItLoadsAnInexistantUuid()
        {
            Assert.Throws<AggregateNotFoundException>(() => _store.Load(Identity<InternalIdentity>.Generate().ToString()));
        }
        
        [Fact]
        public void TestItCanFilterDomainEventStreamByPlayhead()
        {
            Identity<InternalIdentity> uuid = Identity<InternalIdentity>.Generate();
            DomainEventStream stream = GenerateSimpleDomainEventStream();
            _store.Append(uuid.ToString(), stream);

            DomainEventStream des = _store.LoadFromPlayhead(uuid.ToString(), 2);
            Assert.Single(des);
        }

        [Fact]
        public void TestItCanAppendToExistantEventStream()
        {
            IIdentity<InternalIdentity> uuid = Identity<InternalIdentity>.Generate();
            DomainEventStream stream = GenerateSimpleDomainEventStream();
            _store.Append(uuid.ToString(), stream);
            Assert.Equal(3, stream.Count());

            _store.Append(uuid.ToString(), DomainEventStream.FromKeyValuePair(new Dictionary<int, DomainMessage>()
            {
                {
                    3,
                    new DomainMessage(Identity<InternalIdentity>.Generate().ToString(), 3, new TestEvent("This is a dumb payload"),
                        DateTimeImmutable.Now())
                },
                {
                    4,
                    new DomainMessage(Identity<InternalIdentity>.Generate().ToString(), 4, new TestEvent("This is another dumb payload"),
                        DateTimeImmutable.Now())
                },
                {
                    5,
                    new DomainMessage(Identity<InternalIdentity>.Generate().ToString(), 5, new TestEvent("This is another dumbshit payload"),
                        DateTimeImmutable.Now())
                },
            }));
            
            Assert.Equal(6, stream.Count());
        }
    
        [Fact]
        public void TestItDoesNotCatchExceptionOnCorruptedEventStreamAppend()
        {
            IIdentity<InternalIdentity> uuid = Identity<InternalIdentity>.Generate();
            DomainEventStream stream = GenerateSimpleDomainEventStream();
            _store.Append(uuid.ToString(), stream);
            Assert.Equal(3, stream.Count());
            
            DomainEventStream stream2 = GenerateSimpleDomainEventStream();

            Assert.Throws<DuplicatedPlayheadException>(() => _store.Append(uuid.ToString(), stream2));
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
}