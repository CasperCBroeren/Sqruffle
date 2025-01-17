﻿using MassTransit;
using Sqruffle.Domain.Products.Events;

namespace Sqruffle.Web.EventListeners
{
    public class WebProductCreatedListener : IConsumer<ProductCreatedEvent>
    {
        public Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            // Put it on a signalr something
            return Task.CompletedTask;
        }
    }
}
