using System;
using AutoMapper;
using CQRS.CommandStack.Domain.Aggregates.Customer;
using CQRS.Infrastructure.Persistence.Dto;

namespace CQRS.Infrastructure.Persistence.Mapping.Profiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile(Func<CustomerAggregateFactory> getFactory)
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(trg => trg.Id, cfg => cfg.MapFrom(src => src.Id))
                .ForMember(trg => trg.Name, cfg => cfg.MapFrom(src => src.Name))
                .ForMember(trg => trg.Phone, cfg => cfg.MapFrom(src => src.Phone))
                .ForMember(trg => trg.Email, cfg => cfg.MapFrom(src => src.Email))
                .ForAllOtherMembers(cfg => cfg.Ignore());

            CreateMap<CustomerDto, Customer>()
                .ConstructUsing(src => getFactory().Restore(src.Id, src.Name, src.Phone, src.Phone));
        }
    }
}
