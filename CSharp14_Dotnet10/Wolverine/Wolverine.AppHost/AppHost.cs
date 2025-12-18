var builder = DistributedApplication.CreateBuilder(args);


var rmq = builder.AddRabbitMQ("rmq")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithManagementPlugin();

builder.AddProject<Projects.WolvorineSample>("wolvorinesample");

builder.AddProject<Projects.WolverineWithRabbitMq>("wolverinewithrabbitmq")
    .WithReference(rmq)
    .WaitFor(rmq);



builder.Build().Run();
