namespace CQRS.CommandStack.Infrastructure
{
    /// <summary>
    /// Шина событий.
    /// </summary>
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent e);
    }
}