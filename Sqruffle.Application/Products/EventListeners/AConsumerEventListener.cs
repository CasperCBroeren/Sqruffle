using MassTransit;
using Sqruffle.Domain.Feature;

namespace Sqruffle.Application.Products.EventListeners
{
    public abstract class AConsumerEventListener<TEvent> : IConsumer<TEvent> where TEvent : class
    { 
        private readonly IFeatureReactionFinder featureReactionFinder;

        public AConsumerEventListener(IFeatureReactionFinder featureReactionFinder)
        { 
            this.featureReactionFinder = featureReactionFinder;
        }
        public async Task Consume(ConsumeContext<TEvent> context)
        {
            var featureReactors = featureReactionFinder.FindAllFeatureReactorsToEvent<TEvent>();

            foreach (var type in featureReactors.OrderBy(x => x.Priority))
            {
                await type.OnEvent(context.Message);
            }
        }
    }
}
