using Sqruffle.Domain.Feature;
using Sqruffle.Domain.General.Events;

namespace Sqruffle.Application.Products.EventListeners
{
    public class DailyCheckConsumerEventListener : AConsumerEventListener<DailyCheckEvent>
    {
        public DailyCheckConsumerEventListener(IFeatureReactionFinder featureReactionFinder) : base(featureReactionFinder)
        {
        }
    }
}
