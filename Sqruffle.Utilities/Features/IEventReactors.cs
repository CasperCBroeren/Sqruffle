namespace Sqruffle.Domain.Feature
{
    public interface IEventReactors<Event>
    {
        public int Priority { get; }
        Task OnEvent(Event message);
    }
}
