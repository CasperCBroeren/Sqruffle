using System.Reflection;

namespace Sqruffle.Domain.Feature
{
    public interface IFeatureReactionFinder
    {
        List<IFeatureReaction<T, DomainModel>> FindImplementationsOfBehavior<T, DomainModel>();
    }
}
