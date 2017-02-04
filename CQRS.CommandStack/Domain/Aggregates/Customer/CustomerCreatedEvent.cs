using System;

namespace CQRS.CommandStack.Domain.Aggregates.Customer
{
    public class CustomerCreatedEvent
    {
        public Guid Id { get; }

        public CustomerCreatedEvent(Guid id)
        {
            Id = id;
        }
    }
}