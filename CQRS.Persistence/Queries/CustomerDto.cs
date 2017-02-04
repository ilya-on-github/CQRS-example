using System;
using CQRS.QueryStack.Queries.CommonModels;

namespace CQRS.Infrastructure.Persistence.Queries
{
    internal class CustomerDto : ICustomer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}