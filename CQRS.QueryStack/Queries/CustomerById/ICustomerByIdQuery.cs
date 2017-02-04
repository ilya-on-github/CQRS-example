using System;
using CQRS.QueryStack.Infrastructure;
using CQRS.QueryStack.Queries.CommonModels;

namespace CQRS.QueryStack.Queries.CustomerById
{
    public interface ICustomerByIdQuery : IQuery<Guid, ICustomer>
    {
    }
}
