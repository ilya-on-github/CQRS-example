using System;
using CQRS.CommandStack.Infrastructure;

namespace CQRS.CommandStack.Domain.Aggregates.Customer
{
    public class CustomerAggregateFactory
    {
        private readonly IEventPublisher _eventPublisher;

        public CustomerAggregateFactory(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public Customer Create(string name, string phone, string email)
        {
            return new Customer(_eventPublisher, name, phone, email);
        }

        public Customer Restore(Guid id, string name, string phone, string email)
        {
            return new Customer(_eventPublisher, id, name, phone, email);
        }
    }
}