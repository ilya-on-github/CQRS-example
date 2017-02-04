using System;
using System.Linq;
using CQRS.QueryStack.Queries.CommonModels;
using CQRS.QueryStack.Queries.CustomerById;

namespace CQRS.Infrastructure.Persistence.Queries
{
    public class CustomerByIdQuery : ICustomerByIdQuery
    {
        private readonly AppDbContext _dbContext;

        public CustomerByIdQuery(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICustomer Execute(Guid criteria)
        {
            return _dbContext.Customers
                .AsNoTracking()
                .Where(x => x.Id == criteria)
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
