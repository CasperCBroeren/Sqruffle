using Sqruffle.Domain.Feature;
using Sqruffle.Domain.Products.Features;
using System.Text.Json.Serialization;

namespace Sqruffle.Domain.Products
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Expires), "Expires")]
    [JsonDerivedType(typeof(OwnershipRegistration), "OwnershipRegistration")]
    [JsonDerivedType(typeof(PeriodicYield), "PeriodicYield")]
    public abstract class AProductFeature: IFeature
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }        
    }
}
