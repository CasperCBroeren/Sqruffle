using MassTransit;
using Sqruffle.Domain.Products.Events;
using Sqruffle.Domain.Feature;

namespace Sqruffle.Application.Products.EventListeners
{
    public class ProductCreatedListener : IConsumer<ProductCreatedEvent>
    { 
        private readonly IFeatureReactionFinder featureReactionFinder;

        public ProductCreatedListener(IFeatureReactionFinder featureReactionFinder)
        { 
            this.featureReactionFinder = featureReactionFinder;
        }
        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var featureReactors = featureReactionFinder.FindAllFeatureReactorsToEvent<ProductCreatedEvent>();

            foreach (var type in featureReactors.OrderBy(x => x.Priority))
            {
                await type.OnEvent(context.Message);
            }
        }
    }
}
