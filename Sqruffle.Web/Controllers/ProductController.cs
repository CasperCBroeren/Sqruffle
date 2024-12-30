using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sqruffle.Data;
using Sqruffle.Domain.Products;
using Sqruffle.Domain.Products.Features;
using Sqruffle.Utilities;

namespace Sqruffle.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ISqruffleDatabase sqruffleDatabase;
        private readonly TimeProvider timeProvider;
        private readonly IBus bus;

        public ProductController(IBus bus, ISqruffleDatabase sqruffleDatabase, TimeProvider timeProvider)
        {
            this.bus = bus;
            this.sqruffleDatabase = sqruffleDatabase;
            this.timeProvider = timeProvider;
        }

        [HttpPost(Name = "AddProducts")]
        public async Task Add(string name)
        {
            await bus.Publish(new AddProductCommand
            {
                Name = name,
                Expires = new Expires() { ExpiresAtUtc = timeProvider.GetUtcNow().AddYears(-1).DateTime.SetKindUtc() },
                RegisterAt = new OwnershipRegistration() { RegisterAt = "EP" }
            });
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<Product>> Get()
        {
            var items = await sqruffleDatabase.Products.Include(x => x.Features).ToListAsync();

            return items;
        }
    }
}
