namespace Sqruffle.Domain.Products.Features
{
    public class Expires: AProductFeature
    { 
        public DateTime ExpiresAtUtc { get; set; }

        public DateTime? ExpiredAtUtc { get; set; }
    }
}
