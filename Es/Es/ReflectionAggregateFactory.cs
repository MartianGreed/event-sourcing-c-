using System;
using Es.Domain;
using Es.Exception;

namespace Es
{
    public class ReflectionAggregateFactory<T> : IAggregateFactory<T> where T : IAggregateRoot 
    {
        public T Create(DomainEventStream stream)
        {
            T aggregateRoot = (T) Activator.CreateInstance(typeof(T));

            if (null == aggregateRoot)
            {
                throw new FailedToInstanciateAggregateRootException();
            }
            
            aggregateRoot.InitializeState(stream);

            return aggregateRoot;
        }
    }
}