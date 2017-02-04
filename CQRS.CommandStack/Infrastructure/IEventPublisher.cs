namespace CQRS.CommandStack.Infrastructure
{
    /// <summary>
    /// �������� ���������� �������.
    /// </summary>
    /// <remarks>
    /// ���������� ������� � ���� �������.
    /// </remarks>
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent e);
    }
}