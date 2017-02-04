using System.Diagnostics;
using CQRS.CommandStack.Domain.Aggregates.Customer;
using CQRS.CommandStack.Infrastructure;

namespace CQRS.Infrastructure.Persistence.ProjectionBuilders
{
    public class CustomerCreatedEventHandler : IHandle<CustomerCreatedEvent>
    {
        public void Handle(CustomerCreatedEvent e)
        {
            Debug.Write($"Customer {e.Id} created.");
        }
    }
}
