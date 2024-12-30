using Sqruffle.Domain.Product.Aspects;

namespace Sqruffle.Domain.Product
{
    public class AddProductCommand
    {
        public string Name { get; set; }
        
        public Expires Expires { get; set; }
        public OwnershipRegistration RegisterAt { get; set; }
    }
}
