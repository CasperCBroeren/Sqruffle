using Sqruffle.Domain.Feature;
using Sqruffle.Domain.Products.Events;

namespace Sqruffle.Application.Products.EventListeners
{
    public class ProductCreatedConsumerEventListener : AConsumerEventListener<ProductCreatedEvent>
    {
        public ProductCreatedConsumerEventListener(IFeatureReactionFinder featureReactionFinder) : base(featureReactionFinder)
        {
        }
    }
}
