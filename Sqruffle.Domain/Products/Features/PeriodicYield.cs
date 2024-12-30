namespace Sqruffle.Domain.Products.Features
{
    public class PeriodicYield : AProductFeature
    {
        public TimeSpan Interval { get; set; }
        public decimal Increase { get; set; }
    }
}
