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
        public List<IEventReactors<TEvent>> FindAllFeatureReactorsToEvent<TEvent>()
        {
            var genericInterfaceType = typeof(IEventReactors<>).MakeGenericType(typeof(TEvent));
            var assembly = Assembly.GetCallingAssembly();
            if (genericInterfaceType == null || assembly == null)
            {
                return Array.Empty<IEventReactors<TEvent>>().ToList();
            }
            
            var implementations = assembly.GetTypes()
                .Where(type =>
                    !type.IsAbstract &&
                    !type.IsInterface &&
                    type.GetInterfaces().Any(i => i == genericInterfaceType))
                .ToList();

            return implementations
                .Select(type => (IEventReactors<TEvent>)ActivatorUtilities.CreateInstance(serviceProvider, type))
                .ToList();
        }
    }
}

