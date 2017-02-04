using System;
using System.Linq;
using CQRS.QueryStack.Queries.CommonModels;
using CQRS.QueryStack.Queries.CustomerByPhone;

namespace CQRS.Infrastructure.Persistence.Queries
{
    public class CustomerByPhoneQuery : ICustomerByPhoneQuery
    {
        private readonly AppDbContext _dbContext;

        public CustomerByPhoneQuery(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICustomer Execute(string criteria)
        {
            return _dbContext.Customers
                .AsNoTracking()
                .Where(x => x.Phone.Equals(criteria, StringComparison.CurrentCultureIgnoreCase))
                .Select(x => new CustomerDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email
                })
                .FirstOrDefault();
        }
    }
}