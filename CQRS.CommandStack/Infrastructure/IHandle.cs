namespace CQRS.CommandStack.Infrastructure
{
    public interface IHandle<in TCommand, out TResult>
    {
        TResult Handle(TCommand command);
    }

    public interface IHandle<in TCommand>
    {
        void Handle(TCommand command);
    }
}