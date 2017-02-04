namespace CQRS.CommandStack.Infrastructure
{
    /// <summary>
    /// Средство публикации событий.
    /// </summary>
    /// <remarks>
    /// Отправляет события в шину событий.
    /// </remarks>
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent e);
    }
}