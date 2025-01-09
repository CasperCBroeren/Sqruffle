using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sqruffle.Data;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.Products;
using Sqruffle.Domain.Products.Events;
using Sqruffle.Domain.Products.Features;

namespace Sqruffle.Application.Products.EventListeners
{
    public class ProductCreatedListener : IConsumer<ProductCreatedEvent>
    { 
        private readonly IFeatureReactionFinder featureReactionFinder;

        public ProductCreatedListener(SqruffleDatabase sqruffleDatabase, IFeatureReactionFinder featureReactionFinder)
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
