using MassTransit;
using Sqruffle.Application;
using Sqruffle.Web.EventListeners;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqruffle(builder.Configuration,
    mt =>
    {
        mt.AddConsumer<WebProductCreatedListener>();
    },
    (rb, rbContext) =>
    {
        rb.ReceiveEndpoint("ProductCreated_Web_queue", e =>
        {
            e.ConfigureConsumer<WebProductCreatedListener>(rbContext);
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
