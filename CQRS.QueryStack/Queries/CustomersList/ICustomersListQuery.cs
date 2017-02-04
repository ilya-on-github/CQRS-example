using CQRS.QueryStack.Infrastructure;
using CQRS.QueryStack.Queries.CommonModels;

namespace CQRS.QueryStack.Queries.CustomersList
{
    public interface ICustomersListQuery : IQuery<PageInfo, Page<ICustomer>>
    {
    }
}
