using System.Data.Entity;
using System.Reflection;
using CQRS.Infrastructure.Persistence.Dto;

namespace CQRS.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("writeModel")
        {

        }

        internal DbSet<CustomerDto> Customers => Set<CustomerDto>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("writeModel");

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(GetType()));
        }
    }
}