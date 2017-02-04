using CQRS.QueryStack.Infrastructure;
using CQRS.QueryStack.Queries.CommonModels;

namespace CQRS.QueryStack.Queries.CustomerByPhone
{
    public interface ICustomerByPhoneQuery : IQuery<string, ICustomer>
    {
    }
}
