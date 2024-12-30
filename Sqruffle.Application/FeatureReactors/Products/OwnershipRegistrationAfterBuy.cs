using MassTransit;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.Product.Events;

namespace Sqruffle.Domain.Product.Features
{
    public class OwnershipRegistrationAfterBuy : IFeatureReaction<ProductCreatedEvent, Product>
    {
        private readonly HttpClient httpClient;
        private readonly IBus bus;

        public OwnershipRegistrationAfterBuy(HttpClient httpClient, IBus bus)
        {
            this.httpClient = httpClient;
            this.bus = bus;
        }
        public int Priority => 1;

        public async Task OnEvent(Product product)
        {
            Console.WriteLine($"I should do an http request or something to tell that we created product {product.Name}.. but I'm a mock");
            var response = await httpClient.GetStringAsync("https://www.watbenjedan.nl/");
            Console.WriteLine(response);
        }
    }
}
