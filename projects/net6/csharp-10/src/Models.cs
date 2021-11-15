namespace SK.Framework;
public record class Person(string FirstName, string LastName);

public record struct Id(int Value);

public class Demo
{
    public string Name { get; set; }

    public int Version { get; set; }
}