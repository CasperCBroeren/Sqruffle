namespace Sqruffle.Domain.Feature
{
    public interface IFeatureReactionFinder
    {
        List<IEventReactors<TEvent>> FindAllFeatureReactorsToEvent<TEvent>();
    }
}
