namespace CQRS.QueryStack.Infrastructure
{
    public interface IQuery<in TCriteria, out TResult>
    {
        TResult Execute(TCriteria criteria);
    }
}
