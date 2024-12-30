using MassTransit;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.Products;
using Sqruffle.Domain.Products.Events;

namespace Sqruffle.Application.Products.FeatureReactors
{
    public class OwnershipRegistrationAfterBuy : IFeatureReaction<ProductCreatedEvent, Product>
    {
        private readonly HttpClient httpClient;

        public OwnershipRegistrationAfterBuy(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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
