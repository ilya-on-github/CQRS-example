using System;

namespace CQRS.QueryStack.Queries.CommonModels
{
    public interface ICustomer
    {
        Guid Id { get; }

        string Name { get; }

        string Email { get; }

        string Phone { get; }
    }
}