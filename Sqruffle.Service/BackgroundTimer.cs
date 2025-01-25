using MassTransit;
using Microsoft.Extensions.Hosting;
using Sqruffle.Domain.General.Events;
namespace Sqruffle.Service
{
    public class BackgroundTimer(IBus bus) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        { 
            while (!stoppingToken.IsCancellationRequested)
            {
                await bus.Publish(new DailyCheckEvent() {  CurrentTimeUtc = DateTime.UtcNow }, stoppingToken);
                await Task.Delay(5000, stoppingToken); // Runs every 5 seconds
            }
        }
    }
}
