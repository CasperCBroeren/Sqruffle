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
        public List<IEventReactor<TEvent>> FindAllFeatureReactorsToEvent<TEvent>(Assembly assembly)
        {
            var genericInterfaceType = typeof(IEventReactor<>).MakeGenericType(typeof(TEvent));            
            if (genericInterfaceType == null || assembly == null)
            {
                return Array.Empty<IEventReactor<TEvent>>().ToList();
            }
            
            var implementations = assembly.GetTypes()
                .Where(type =>
                    !type.IsAbstract &&
                    !type.IsInterface &&
                    type.GetInterfaces().Any(i => i == genericInterfaceType))
                .ToList();

            return implementations
                .Select(type => (IEventReactor<TEvent>)ActivatorUtilities.CreateInstance(serviceProvider, type))
                .ToList();
        }
    }
}

