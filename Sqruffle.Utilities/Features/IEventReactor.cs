namespace Sqruffle.Domain.Feature
{
    public interface IEventReactor<Event>
    {
        public int Priority { get; }
        Task OnEvent(Event message);
    }
}
