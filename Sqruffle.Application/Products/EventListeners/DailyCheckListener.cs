using MassTransit;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.General.Events;

namespace Sqruffle.Application.Products.EventListeners
{
    public class DailyCheckListener : IConsumer<DailyCheckEvent>
    {
        private readonly IFeatureReactionFinder featureReactionFinder; 

        public DailyCheckListener(IFeatureReactionFinder featureReactionFinder, TimeProvider timeProvider)
        {
            this.featureReactionFinder = featureReactionFinder; 
        }
        public async Task Consume(ConsumeContext<DailyCheckEvent> context)
        {
            var featureReactors = featureReactionFinder.FindAllFeatureReactorsToEvent<DailyCheckEvent>();
 
            foreach (var type in featureReactors.OrderBy(x => x.Priority))
            {
                await type.OnEvent(context.Message);
            }
        }
    }
}
