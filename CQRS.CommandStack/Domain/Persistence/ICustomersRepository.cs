using System;
using CQRS.CommandStack.Domain.Aggregates.Customer;

namespace CQRS.CommandStack.Domain.Persistence
{
    public interface ICustomersRepository
    {
        Customer Get(Guid id);

        void Add(Customer customer);

        void Update(Customer customer);

        void Delete(Guid id);
    }
}