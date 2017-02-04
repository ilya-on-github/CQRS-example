using AutoMapper;
using CQRS.CommandStack.CommandHandlers;

namespace CQRS.Api.Customers
{
    public class CustomersMappingProfile : Profile
    {
        public CustomersMappingProfile()
        {
            CreateMap<CreateCustomerCommandDto, CreateCustomerCommand>()
                .ConstructUsing(src => new CreateCustomerCommand(src.Name, src.Phone, src.Email));
        }
    }
}