using System;

var person = new Person();

//Delegate Mapping
DelegateWithMatchingSignature methodCall = new(person.MethodIWantToCall);

//Call method using Delegate
Console.WriteLine(methodCall("Newman Croos"));

person.Name = "Harry";

//Map EventHandler in Person Class
//person.Shout = Harry_Shout;   // Regular Event Handler 

person.Shout += Harry_Shout;   // Calling multicast delegate Event
person.Shout += Harry_Shout1;

person.Poke();
person.Poke();
person.Poke();
person.Poke();
static void Harry_Shout(object? sender, EventArgs e)
{

    if (sender is null) return;

    if (sender is not Person p) return;

    Console.WriteLine($"{p.Name} is this angery : {p.AngerLevel}");
}

static void Harry_Shout1(object? sender, EventArgs e)
{

    if (sender is null) return;

    if (sender is not Person p) return;

    Console.WriteLine($"{p.Name} is this angery : {p.AngerLevel}");
}


Console.ReadLine();

//Delegate Declaration
delegate int DelegateWithMatchingSignature(string s);

//Declare EventHandler
public delegate void EventHandler(object? sender, EventArgs e);



public class Person
{
    public string Name { get; set; }
    //public EventHandler? Shout;

    //Changing it to Multicast delegate
    public event EventHandler Shout;

    public int AngerLevel;
    public int MethodIWantToCall(string input)
    {
        return input.Length;
    }

    public void Poke()
    {
        AngerLevel++;
        if (AngerLevel < 3) return;

        if (Shout is not null)
        {
            Shout(this, EventArgs.Empty);
        }
    }
}
