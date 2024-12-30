using Sqruffle.Domain.Feature;

namespace Sqruffle.Domain.Product
{
    public abstract class AProductFeature: IFeature
    {
        public int Id { get; set; }
        public string Type { get; set; } 
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
