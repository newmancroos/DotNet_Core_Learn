using Wolverine;
using Wolverine.RabbitMQ;
using WolverineWithRabbitMq.Feature.Register;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseWolverine(options => 
{ 
    options.UseRabbitMqUsingNamedConnection("rmq")
    .AutoProvision()
    .UseConventionalRouting();

    options.Policies.DisableConventionalLocalRouting(); // This will publish it to Rabbitmq other wise it will be handled locally
});


builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource("Wolverine"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapPost("users", async (RegsiterUser command, IMessageBus bus) =>
{
    var result = await bus.InvokeAsync<Guid>(command);
    return Results.Ok(new { result });
});
app.UseHttpsRedirection();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
