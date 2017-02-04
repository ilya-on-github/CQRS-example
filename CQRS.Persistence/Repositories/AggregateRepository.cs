using CQRS.Infrastructure.Persistence.Mapping;

namespace CQRS.Infrastructure.Persistence.Repositories
{
    public class AggregateRepository
    {
        protected readonly PersistenceMapper Mapper;

        protected AggregateRepository(PersistenceMapper mapper)
        {
            Mapper = mapper;
        }
    }
}