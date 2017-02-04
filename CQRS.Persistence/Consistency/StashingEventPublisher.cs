using System.Collections.Generic;
using CQRS.CommandStack.Infrastructure;

namespace CQRS.Infrastructure.Persistence.Consistency
{
    /// <summary>
    /// Накапливает события и публикует их в шину при вызове <see cref="Flush"/>.
    /// События - это свершившийся факт. При использовании UnitOfWork события порождаются агрегатами, 
    /// но до фиксации транзакции они не являются свершившимся фактом.
    /// Другими словами, события необходимо публиковать только после коммита транзакции.
    /// И этот тип позволяет это сделать.
    /// </summary>
    public class StashingEventPublisher : IEventPublisher
    {
        public StashingEventPublisher(IEventBus bus)
        {
            _eventBus = bus;
        }

        private readonly IEventBus _eventBus;
        private readonly List<dynamic> _eventStash = new List<dynamic>();

        public void Publish<TEvent>(TEvent e)
        {
            if (e != null)
                _eventStash.Add(e);
        }

        public void Flush()
        {
            foreach (var e in _eventStash)
            {
                _eventBus.Publish(e);
            }
        }
    }
}