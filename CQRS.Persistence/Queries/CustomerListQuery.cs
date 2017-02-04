using System.Linq;
using CQRS.QueryStack.Queries;
using CQRS.QueryStack.Queries.CommonModels;
using CQRS.QueryStack.Queries.CustomersList;

namespace CQRS.Infrastructure.Persistence.Queries
{
    public class CustomerListQuery : ICustomersListQuery
    {
        private readonly AppDbContext _dbContext;

        public CustomerListQuery(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Page<ICustomer> Execute(PageInfo criteria)
        {
            var query = _dbContext.Customers
                .AsNoTracking();

            var total = query.Count();

            var items = query
                .OrderBy(x => x.Id)
                .Skip((criteria.Page - 1) * criteria.Size)
                .Take(criteria.Size)
                .Select(x => new CustomerDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email
                })
                .ToList();

            return new Page<ICustomer>(criteria.Page, criteria.Size, items, total);
        }
    }
}