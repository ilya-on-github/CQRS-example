namespace CQRS.CommandStack.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
