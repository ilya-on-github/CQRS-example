namespace CQRS.Api.Customers
{
    public class CreateCustomerCommandDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}