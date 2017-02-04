using System;
using System.Web.Http;
using CQRS.Api.Infrastructure;
using CQRS.CommandStack.CommandHandlers;
using CQRS.CommandStack.Infrastructure;
using CQRS.QueryStack.Queries;
using CQRS.QueryStack.Queries.CustomerById;
using CQRS.QueryStack.Queries.CustomersList;

namespace CQRS.Api.Customers
{
    [RoutePrefix("api/customers")]
    public class CustomersApiController : ApiControllerBase
    {
        private readonly ICustomerByIdQuery _customerByIdQuery;
        private readonly ICustomersListQuery _customersListQuery;

        public CustomersApiController(ICommandDispatcher commandDispatcher, ApiMapper mapper,
            ICustomerByIdQuery customerByIdQuery, ICustomersListQuery customersListQuery)
            : base(commandDispatcher, mapper)
        {
            _customerByIdQuery = customerByIdQuery;
            _customersListQuery = customersListQuery;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetCustomers([FromUri] int page, [FromUri] int size)
        {
            var result = _customersListQuery.Execute(new PageInfo(page, size));

            return Ok(result);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateCustomer([FromBody]CreateCustomerCommandDto dto)
        {
            var command = Mapper.Map<CreateCustomerCommandDto, CreateCustomerCommand>(dto);

            var customerId = CommandDispatcher.Handle<CreateCustomerCommand, Guid>(command);

            var customer = _customerByIdQuery.Execute(customerId);

            return Ok(customer);
        }
    }
}