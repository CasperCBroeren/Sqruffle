namespace Sqruffle.Domain.Products
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<AProductFeature> Features { get; set; }

    }
}
