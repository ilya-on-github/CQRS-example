using System.Collections.Generic;
using CQRS.CommandStack.Infrastructure;

namespace CQRS.Infrastructure.Persistence.Consistency
{
    /// <summary>
    /// ����������� ������� � ��������� �� � ���� ��� ������ <see cref="Flush"/>.
    /// ������� - ��� ������������ ����. ��� ������������� UnitOfWork ������� ����������� ����������, 
    /// �� �� �������� ���������� ��� �� �������� ������������ ������.
    /// ������� �������, ������� ���������� ����������� ������ ����� ������� ����������.
    /// � ���� ��� ��������� ��� �������.
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