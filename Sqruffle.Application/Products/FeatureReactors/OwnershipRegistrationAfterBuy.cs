using Microsoft.EntityFrameworkCore;
using Sqruffle.Data;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.Products.Events;
using Sqruffle.Domain.Products.Features;

namespace Sqruffle.Application.Products.FeatureReactors
{
    public class OwnershipRegistrationAfterBuy : IEventReactor<ProductCreatedEvent>
    {
        private readonly SqruffleDatabase sqruffleDatabase;
        private readonly HttpClient httpClient;

        public OwnershipRegistrationAfterBuy(SqruffleDatabase sqruffleDatabase, HttpClient httpClient)
        {
            this.sqruffleDatabase = sqruffleDatabase;
            this.httpClient = httpClient;
        }
        public int Priority => 1;

        public async Task OnEvent(ProductCreatedEvent message)
        {
            var product = await sqruffleDatabase.Products
                        .Include(p => p.Features)
                        .Where(p => p.Features.OfType<OwnershipRegistration>().Any())
                        .FirstAsync(p => p.Id == message.ProductId);

            Console.WriteLine($"I should do an http request or something to tell that we created product {product.Name}.. but I'm a mock");
            var response = await httpClient.GetStringAsync("https://www.watbenjedan.nl/");
            Console.WriteLine(response);
        }
    }
}
