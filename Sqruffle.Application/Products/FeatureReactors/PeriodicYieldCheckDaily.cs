using Microsoft.EntityFrameworkCore;
using Sqruffle.Data;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.General.Events;
using Sqruffle.Domain.Products.Features;

namespace Sqruffle.Application.Products.FeatureReactors
{
    public class PeriodicYieldCheckDaily : IEventReactor<DailyCheckEvent>
    {
        private readonly SqruffleDatabase sqruffleDatabase;

        public PeriodicYieldCheckDaily(SqruffleDatabase sqruffleDatabase)
        {
            this.sqruffleDatabase = sqruffleDatabase;
        }
        public int Priority => 2;

        public async Task OnEvent(DailyCheckEvent item)
        {
            var productsToApplyYield = await sqruffleDatabase.Products
                         .Where(p => p.Features.OfType<PeriodicYield>().Any())
                         .ToListAsync();
            if (productsToApplyYield.Any())
            {
                Console.WriteLine($"Applying yield for {productsToApplyYield.Count} products");
            }
            else
            {
                Console.WriteLine("NO yield to apply");
            }
        }
    }
}
