using MassTransit;
using Sqruffle.Domain.Feature;
using System.Reflection;

namespace Sqruffle.Utilities.Analyzer
{
    public abstract class AConsumerEventListener<TEvent> : IConsumer<TEvent> where TEvent : class
    {
        private readonly IFeatureReactionFinder featureReactionFinder;
        protected Assembly? assembly;

        public AConsumerEventListener(IFeatureReactionFinder featureReactionFinder)
        {
            this.featureReactionFinder = featureReactionFinder;
        }
        public async Task Consume(ConsumeContext<TEvent> context)
        {
            var featureReactors = featureReactionFinder.FindAllFeatureReactorsToEvent<TEvent>(this.assembly!);

            foreach (var type in featureReactors.OrderBy(x => x.Priority))
            {
                await type.OnEvent(context.Message);
            }
        }
    }
}
