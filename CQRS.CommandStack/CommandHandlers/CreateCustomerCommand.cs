namespace CQRS.CommandStack.CommandHandlers
{
    public class CreateCustomerCommand
    {
        public CreateCustomerCommand(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }

        public string Name { get; }

        public string Email { get; }

        public string Phone { get; }
    }
}