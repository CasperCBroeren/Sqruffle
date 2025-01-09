using Microsoft.EntityFrameworkCore;
using Sqruffle.Data;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.General.Events;
using Sqruffle.Domain.Products.Features;

namespace Sqruffle.Application.Products.FeatureReactors
{
    public class ExpiresCheckDaily : IEventReactor<DailyCheckEvent>
    {
        private readonly SqruffleDatabase sqruffleDatabase;

        public ExpiresCheckDaily(SqruffleDatabase sqruffleDatabase)
        {
            this.sqruffleDatabase = sqruffleDatabase;
        }

        public int Priority => 1;

        public async Task OnEvent(DailyCheckEvent item)
        { 
            var expiredProducts = await sqruffleDatabase.Products
                            .Where(p => p.Features.OfType<Expires>().Any())
                            .Where(p => !p.Features.OfType<Expires>().First().ExpiredAtUtc.HasValue && p.Features.OfType<Expires>().First().ExpiresAtUtc < item.CurrentTimeUtc)
                            .Include(p => p.Features)
                            .ToListAsync();
            if (expiredProducts.Any())
            {
                foreach (var p in expiredProducts)
                {
                    var feature = p.Features.OfType<Expires>().First();
                    feature.ExpiredAtUtc = item.CurrentTimeUtc;
                }
                await sqruffleDatabase.SaveChangesAsync();
                Console.WriteLine($"{expiredProducts.Count} Product expired ");
            }
            else
            {
                Console.WriteLine("No product expired");
            }
        }
    }
}
