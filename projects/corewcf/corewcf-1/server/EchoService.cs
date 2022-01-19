using Contracts;

public class EchoService : IEchoService
{
    public string Echo(string text)
    {
        System.Console.WriteLine($"Received {text} from client!");
        return text;
    }

    public string ComplexEcho(EchoMessage text)
    {
        System.Console.WriteLine($"Received {text.Text} from client!");
        return text.Text;
    }
}