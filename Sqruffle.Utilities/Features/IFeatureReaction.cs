namespace Sqruffle.Domain.Feature
{
    public interface IFeatureReaction<Aspect, DomainObject>
    {
        public int Priority { get; }
        Task OnEvent(DomainObject item);
    }
}
