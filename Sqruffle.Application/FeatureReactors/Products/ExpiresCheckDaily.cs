using Sqruffle.Domain.Feature;
using Sqruffle.Domain.Product.Events;

namespace Sqruffle.Domain.Product.Features
{
    public class ExpiresCheckDaily : IFeatureReaction<DailyCheck, Product>
    {
        public int Priority => 1;

        public Task OnEvent(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
