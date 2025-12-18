using Wolverine;
using WolvorineSample.Feature.Register;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Host.UseWolverine();
// Add services to the container.


var app = builder.Build();

app.MapDefaultEndpoints();


// Configure the HTTP request pipeline.

app.MapPost("register", async (RegsiterUser command, IMessageBus bus) =>
{
    await bus.InvokeAsync(command);
    return "output";
});
app.UseHttpsRedirection();



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
