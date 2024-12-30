using MassTransit;
using Sqruffle.Data;
using Sqruffle.Web.EventListeners;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqruffle(builder.Configuration,
    mt =>
    {
        mt.AddConsumer<WebProductCreatedListener>();
    },
    rb =>
    {
        rb.ReceiveEndpoint("ProductCreated_Web_queue", e =>
        {
            e.ConfigureConsumer<WebProductCreatedListener>(context);
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
