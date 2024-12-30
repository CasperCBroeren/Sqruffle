using MassTransit;
using Sqruffle.Data;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.General.Events;

namespace Sqruffle.Application.Products.EventListeners
{
    public class DailyCheckListener : IConsumer<DailyCheckEvent>
    {
        private readonly SqruffleDatabase sqruffleDatabase;
        private readonly IFeatureReactionFinder behaviorFinder;
        private readonly TimeProvider timeProvider;

        public DailyCheckListener(SqruffleDatabase sqruffleDatabase, IFeatureReactionFinder behaviorFinder, TimeProvider timeProvider)
        {
            this.sqruffleDatabase = sqruffleDatabase;
            this.behaviorFinder = behaviorFinder;
            this.timeProvider = timeProvider;
        }
        public async Task Consume(ConsumeContext<DailyCheckEvent> context)
        {
            var behavior = behaviorFinder.FindImplementationsOfBehavior<DailyCheckEvent, DateTimeOffset>();
 
            foreach (var type in behavior.OrderBy(x => x.Priority))
            {
                await type.OnEvent(timeProvider.GetUtcNow());
            }
        }
    }
}
