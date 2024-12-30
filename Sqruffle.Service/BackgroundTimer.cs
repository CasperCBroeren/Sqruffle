using MassTransit;
using Microsoft.Extensions.Hosting;
using Sqruffle.Domain.General.Events;
namespace Sqruffle.Service
{
    public class BackgroundTimer : BackgroundService
    {
        private readonly IBus bus;

        public BackgroundTimer(IBus bus)
        {
            this.bus = bus;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        { 
            while (!stoppingToken.IsCancellationRequested)
            {
                await bus.Publish(new DailyCheckEvent());
                await Task.Delay(5000, stoppingToken); // Runs every 5 seconds
            }
        }
    }
}
