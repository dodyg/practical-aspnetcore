namespace SK.Framework;
public record class Person(string FirstName, string LastName);

public record struct Id(int Value);

public record Club(Id Id, string Name);

public class Demo
{
    public string Name { get; set; }

    public int Version { get; set; }
}