using Wolverine;
using Wolverine.Feature.Register;

var builder = WebApplication.CreateBuilder(args);

//builder.AddServiceDefaults();
//builder.Services.AddOpenApi();
// Add services to the container.

builder.Host.UseWolverine();
var app = builder.Build();

// Configure the HTTP request pipeline.

//app.MapPost("users", (RegisterUser command, IMessageBus bus) =>
//{
//    return bus.InvokeAsync(command);
//});

app.UseHttpsRedirection();


app.Run();


