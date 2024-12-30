using Sqruffle.Domain.Feature;
using Sqruffle.Domain.Product.Events;

namespace Sqruffle.Domain.Product.Features
{
    public class PeriodicYieldCheckDaily : IFeatureReaction<DailyCheck, Product>
    {
        public int Priority => 2;

        public Task OnEvent(Product p)
        {
            throw new NotImplementedException();
        }
    }
}
