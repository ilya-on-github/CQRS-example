using System;
using CQRS.CommandStack.Domain.Aggregates.Customer;
using CQRS.CommandStack.Domain.Persistence;
using CQRS.CommandStack.Infrastructure;
using CQRS.QueryStack.Queries.CustomerByPhone;

namespace CQRS.CommandStack.CommandHandlers
{
    public class CreateCustomerCommandHandler : IHandle<CreateCustomerCommand, Guid>
    {
        private readonly ICustomersRepository _repository;
        private readonly CustomerAggregateFactory _concreteAggregateFactory;
        private readonly ICustomerByPhoneQuery _customerByPhoneQuery;

        public CreateCustomerCommandHandler(ICustomersRepository repository,
            CustomerAggregateFactory concreteAggregateFactory, ICustomerByPhoneQuery customerByPhoneQuery)
        {
            _repository = repository;
            _concreteAggregateFactory = concreteAggregateFactory;
            _customerByPhoneQuery = customerByPhoneQuery;
        }

        public Guid Handle(CreateCustomerCommand command)
        {
            var customerWithTheSamePhone = _customerByPhoneQuery.Execute(command.Phone);
            if (customerWithTheSamePhone != null)
                throw new ArgumentException($"Телефон {command.Phone} уже привязан к другому аккаунту");

            var customer = _concreteAggregateFactory.Create(command.Name, command.Phone, command.Email);

            _repository.Add(customer);

            return customer.Id;
        }
    }
}