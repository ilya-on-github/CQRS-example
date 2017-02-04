using System.Data.Entity.ModelConfiguration;
using CQRS.Infrastructure.Persistence.Dto;

namespace CQRS.Infrastructure.Persistence.EntityTypeConfigurations
{
    internal class CustomerDtoConfiguration : EntityTypeConfiguration<CustomerDto>
    {
        public CustomerDtoConfiguration()
        {
            ToTable("Customers");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.Phone).HasColumnName("Phone");
            Property(x => x.Email).HasColumnName("Email");
        }
    }
}