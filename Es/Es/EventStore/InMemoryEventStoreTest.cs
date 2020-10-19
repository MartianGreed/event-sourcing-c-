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
            IIdentity uuid = Identity.Generate();
            DomainEventStream stream = GenerateSimpleDomainEventStream();
            _store.Append(uuid, stream);

            DomainEventStream des = _store.Load(uuid);
            Assert.Equal(3, des.Count());
        }

        [Fact]
        public void TestItThrowsAnExceptionIfItLoadsAnInexistantUuid()
        {
            Assert.Throws<AggregateNotFoundException>(() => _store.Load(Identity.Generate()));
        }
        
        [Fact]
        public void TestItCanFilterDomainEventStreamByPlayhead()
        {
            IIdentity uuid = Identity.Generate();
            DomainEventStream stream = GenerateSimpleDomainEventStream();
            _store.Append(uuid, stream);

            DomainEventStream des = _store.LoadFromPlayhead(uuid, 2);
            Assert.Single(des);
        }

        [Fact]
        public void TestItCanAppendToExistantEventStream()
        {
            IIdentity uuid = Identity.Generate();
            DomainEventStream stream = GenerateSimpleDomainEventStream();
            _store.Append(uuid, stream);
            Assert.Equal(3, stream.Count());

            _store.Append(uuid, DomainEventStream.FromKeyValuePair(new Dictionary<int, DomainMessage>()
            {
                {
                    3,
                    new DomainMessage(Identity.Generate().ToString(), 3, "This is a dumb payload",
                        DateTimeImmutable.Now())
                },
                {
                    4,
                    new DomainMessage(Identity.Generate().ToString(), 4, "This is another dumb payload",
                        DateTimeImmutable.Now())
                },
                {
                    5,
                    new DomainMessage(Identity.Generate().ToString(), 5, "This is another dumbshit payload",
                        DateTimeImmutable.Now())
                },
            }));
            
            Assert.Equal(6, stream.Count());
        }
    
        [Fact]
        public void TestItDoesNotCatchExceptionOnCorruptedEventStreamAppend()
        {
            IIdentity uuid = Identity.Generate();
            DomainEventStream stream = GenerateSimpleDomainEventStream();
            _store.Append(uuid, stream);
            Assert.Equal(3, stream.Count());
            
            DomainEventStream stream2 = GenerateSimpleDomainEventStream();

            Assert.Throws<DuplicatedPlayheadException>(() => _store.Append(uuid, stream2));
        }

        private DomainEventStream GenerateSimpleDomainEventStream()
        {
            return DomainEventStream.FromKeyValuePair(new Dictionary<int, DomainMessage>()
            {
                {0, new DomainMessage(Identity.Generate().ToString(), 0, "This is a dumb payload", DateTimeImmutable.Now())},
                {1, new DomainMessage(Identity.Generate().ToString(), 1, "This is another dumb payload", DateTimeImmutable.Now())},
                {2, new DomainMessage(Identity.Generate().ToString(), 2, "This is another dumbshit payload", DateTimeImmutable.Now())},
            });
        }
    }
}