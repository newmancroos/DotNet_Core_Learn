using System.Runtime.CompilerServices;

namespace Wolverine.Feature.Register;

public class RegisterUserHandler
{
    public async Task Handle(RegisterUser command)
    {
        //We can save it to the database or something
        await Task.Delay(1);
        Console.WriteLine($"Registered user {command.Email} - {command.FirstName} {command.LastName}");


    }
}
