namespace WolvorineSample.Feature.Register;

public class RegisterUserHandler
{
    public async Task<UserRegsitered> Handle(RegsiterUser command)
    {
        // Handle user registration logic here
        Console.WriteLine($"Registering user: {command.FirstName} {command.LastName} with email {command.Email}");
        Guid Id = Guid.NewGuid();
        return (new UserRegsitered(Id));
    }
}
