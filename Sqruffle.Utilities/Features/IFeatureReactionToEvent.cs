namespace Sqruffle.Domain.Feature
{
    public interface IFeatureReactionToEvent<TFeature, TEvent, DomainObject>
    {
        public int Priority { get; }
        Task OnEvent(DomainObject item);
    }
}
