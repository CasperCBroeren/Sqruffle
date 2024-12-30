using Sqruffle.Domain.Products.Features;

namespace Sqruffle.Domain.Products
{
    public class AddProductCommand
    {
        public string Name { get; set; }
        
        public Expires Expires { get; set; }
        public OwnershipRegistration RegisterAt { get; set; }
    }
}
