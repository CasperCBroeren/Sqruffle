using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Sqruffle.Domain.Feature
{
    public class FeatureReactionFinder : IFeatureReactionFinder
    {
        private readonly IServiceProvider serviceProvider;

        public FeatureReactionFinder(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public List<IFeatureReaction<T, DomainModel>> FindAllFeatureReactorsToEvent<T, DomainModel>()
        {
            var genericInterfaceType = typeof(IFeatureReaction<,>).MakeGenericType(typeof(T), typeof(DomainModel));
            var assembly = Assembly.GetCallingAssembly();
            if (genericInterfaceType == null || assembly == null)
            {
                return Array.Empty<IFeatureReaction<T, DomainModel>>().ToList();
            }
            
            var implementations = assembly.GetTypes()
                .Where(type =>
                    !type.IsAbstract &&
                    !type.IsInterface &&
                    type.GetInterfaces().Any(i => i == genericInterfaceType))
                .ToList();

            return implementations
                .Select(type => (IFeatureReaction<T, DomainModel>)ActivatorUtilities.CreateInstance(serviceProvider, type))
                .ToList();
        }
    }
}

