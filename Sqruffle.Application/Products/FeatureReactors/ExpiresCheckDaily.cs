using Microsoft.EntityFrameworkCore;
using Sqruffle.Data;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.General.Events;
using Sqruffle.Domain.Products.Features;
using Sqruffle.Utilities;

namespace Sqruffle.Application.Products.FeatureReactors
{
    public class ExpiresCheckDaily : IFeatureReaction<DailyCheckEvent, DateTimeOffset>
    {
        private readonly SqruffleDatabase sqruffleDatabase;

        public ExpiresCheckDaily(SqruffleDatabase sqruffleDatabase)
        {
            this.sqruffleDatabase = sqruffleDatabase;
        }

        public int Priority => 1;

        public async Task OnEvent(DateTimeOffset item)
        {
            var datetime = item.DateTime.SetKindUtc();
            var expiredProducts = await sqruffleDatabase.Products
                            .Where(p => p.Features.OfType<Expires>().Any())
                            .Where(p => !p.Features.OfType<Expires>().First().ExpiredAtUtc.HasValue && p.Features.OfType<Expires>().First().ExpiresAtUtc < datetime)
                            .Include(p => p.Features)
                            .ToListAsync();
            if (expiredProducts.Any())
            {
                foreach (var p in expiredProducts)
                {
                    var feature = p.Features.OfType<Expires>().First();
                    feature.ExpiredAtUtc = datetime;
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
