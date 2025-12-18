using Wolverine;

namespace WolverineWithRabbitMq.Feature.Register;

public class RegisterUserHandler
{
    public async Task<Guid> Handle(RegsiterUser command, IMessageBus bus)
    {
        // Handle user registration logic here
        Console.WriteLine($"Registering user: {command.FirstName} {command.LastName} with email {command.Email}");
        Guid Id = Guid.NewGuid();
        await bus.PublishAsync (new UserRegsitered(Id));
            
        return Id;
    }
}
