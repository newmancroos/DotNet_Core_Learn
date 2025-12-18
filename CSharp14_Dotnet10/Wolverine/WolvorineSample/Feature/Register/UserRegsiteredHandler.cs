namespace WolvorineSample.Feature.Register
{
    public class UserRegsiteredHandler
    {
        public async Task Handle(UserRegsitered notification)
        {
            // Handle user registered event logic here
            Console.WriteLine($"User registered with ID: {notification.Id}");
        }
    }
}
