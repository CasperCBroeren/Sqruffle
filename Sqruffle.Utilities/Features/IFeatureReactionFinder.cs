using System.Reflection;

namespace Sqruffle.Domain.Feature
{
    public interface IFeatureReactionFinder
    {
        List<IEventReactor<TEvent>> FindAllFeatureReactorsToEvent<TEvent>(Assembly assembly);
    }
}
