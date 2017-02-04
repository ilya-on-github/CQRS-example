namespace CQRS.CommandStack.Infrastructure
{
    public interface ICommandDispatcher
    {
        void Handle<TCommand>(TCommand command);

        TResult Handle<TCommand, TResult>(TCommand command);
    }
}