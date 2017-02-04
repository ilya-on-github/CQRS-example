using System;
using CQRS.CommandStack.Infrastructure;

namespace CQRS.CommandStack.Domain.Aggregates.Customer
{
    public class Customer : Aggregate
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Phone { get; }
        public string Email { get; }

        internal Customer(IEventPublisher publisher, string name, string phone, string email)
            : base(publisher)
        {
            Id = Guid.NewGuid();

            Name = name;
            Phone = phone;
            Email = email;

            Publish(new CustomerCreatedEvent(Id));
        }

        internal Customer(IEventPublisher publisher, Guid id, string name, string phone, string email)
            : base(publisher)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
        }
    }
}
