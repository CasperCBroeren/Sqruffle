namespace Sqruffle.Domain.Product.Aspects
{
    public class PeriodicYield : AProductFeature
    {
        public TimeSpan Interval { get; set; }
        public decimal Increase { get; set; }
    }
}
