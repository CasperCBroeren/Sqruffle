using MassTransit;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.General.Events;

namespace Sqruffle.Application.Products.EventListeners
{
    public class DailyCheckListener : IConsumer<DailyCheckEvent>
    {
        private readonly IFeatureReactionFinder behaviorFinder;
        private readonly TimeProvider timeProvider;

        public DailyCheckListener(IFeatureReactionFinder behaviorFinder, TimeProvider timeProvider)
        {
            this.behaviorFinder = behaviorFinder;
            this.timeProvider = timeProvider;
        }
        public async Task Consume(ConsumeContext<DailyCheckEvent> context)
        {
            var behavior = behaviorFinder.FindAllFeatureReactorsToEvent<DailyCheckEvent, DateTimeOffset>();
 
            foreach (var type in behavior.OrderBy(x => x.Priority))
            {
                await type.OnEvent(timeProvider.GetUtcNow());
            }
        }
    }
}
