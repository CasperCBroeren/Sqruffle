using MassTransit;
using Microsoft.EntityFrameworkCore;
using Sqruffle.Data;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.Product;
using Sqruffle.Domain.Product.Events;

namespace Sqruffle.Application.EventListeners.Product
{
    public class ProductCreatedListener : IConsumer<ProductCreatedEvent>
    {
        private readonly SqruffleDatabase sqruffleDatabase;
        private readonly IFeatureReactionFinder behaviorFinder;

        public ProductCreatedListener(SqruffleDatabase sqruffleDatabase, IFeatureReactionFinder behaviorFinder)
        {
            this.sqruffleDatabase = sqruffleDatabase;
            this.behaviorFinder = behaviorFinder;
        }
        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var behavior = behaviorFinder.FindImplementationsOfBehavior<ProductCreatedEvent, Product>();
            var product = sqruffleDatabase.Products
                                    .Include(p => p.Aspects)
                                    .First(p => p.Id == context.Message.ProductId);

            foreach (var type in behavior.OrderBy(x => x.Priority))
            {
                await type.OnEvent(product);
            }
        }
    }
}
