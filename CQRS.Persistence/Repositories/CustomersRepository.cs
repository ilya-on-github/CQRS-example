using System;
using System.Linq;
using CQRS.CommandStack.Domain.Aggregates.Customer;
using CQRS.CommandStack.Domain.Persistence;
using CQRS.Infrastructure.Persistence.Dto;
using CQRS.Infrastructure.Persistence.Mapping;

namespace CQRS.Infrastructure.Persistence.Repositories
{
    internal class CustomersRepository : AggregateRepository, ICustomersRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomersRepository(AppDbContext dbContext, PersistenceMapper mapper)
            : base(mapper)
        {
            _dbContext = dbContext;
        }

        public Customer Get(Guid id)
        {
            var dto = _dbContext.Customers.FirstOrDefault(x => x.Id.Equals(id));

            return Mapper.Map<CustomerDto, Customer>(dto);
        }

        public void Add(Customer customer)
        {
            var dto = Mapper.Map<Customer, CustomerDto>(customer);

            _dbContext.Customers.Add(dto);
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}