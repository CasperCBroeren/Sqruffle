using MassTransit;
using Sqruffle.Data;
using Sqruffle.Domain.Products;
using Sqruffle.Domain.Products.Events;

namespace Sqruffle.Application.Products.CommandHandlers
{
    public class AddProductConsumer : IConsumer<AddProductCommand>
    {
        private readonly ISqruffleDatabase sqruffleDatabase;

        public AddProductConsumer(ISqruffleDatabase sqruffleDatabase)
        {
            this.sqruffleDatabase = sqruffleDatabase;
        }
        public async Task Consume(ConsumeContext<AddProductCommand> context)
        {
            var entity = new Product() { Id = NewId.NextGuid(), Name = context.Message.Name, Features = [context.Message.Expires, context.Message.RegisterAt] };
            await sqruffleDatabase.Products.AddAsync(entity);
            await sqruffleDatabase.SaveChangesAsync(context.CancellationToken);

            await context.Publish(new ProductCreatedEvent(entity.Id));
        }
    }
}
