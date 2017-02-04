namespace CQRS.CommandStack.Infrastructure
{
    /// <summary>
    /// Базовый тип агрегата. Содержит метод публикации событий.
    /// </summary>
    public abstract class Aggregate
    {
        private readonly IEventPublisher _eventPublisher;

        protected Aggregate(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        protected void Publish<TEvent>(TEvent e)
        {
            _eventPublisher.Publish(e);
        }
    }
}