using CQRS.CommandStack.Infrastructure;

namespace CQRS.Infrastructure.Persistence.Consistency
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StashingEventPublisher _eventPublisher;
        private readonly AppDbContext _dbContext;

        public UnitOfWork(StashingEventPublisher eventPublisher, AppDbContext dbContext)
        {
            _eventPublisher = eventPublisher;
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
            _eventPublisher.Flush();
        }
    }
}