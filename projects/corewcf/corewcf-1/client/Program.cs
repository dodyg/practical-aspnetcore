using System.ServiceModel;

const string BasicHttpEndPointAddress = @"http://localhost:8080/basichttp";

SimpleEcho();
ComplexEcho();

void SimpleEcho()
{
    var factory = new ChannelFactory<Contracts.IEchoService>(new BasicHttpBinding(), new EndpointAddress(BasicHttpEndPointAddress));
    factory.Open();
    var channel = factory.CreateChannel();
    ((IClientChannel)channel).Open();
    Console.WriteLine("http Echo(\"Hello\") => " + channel.Echo("Hello"));
    ((IClientChannel)channel).Close();
    factory.Close();
}

void ComplexEcho()
{
    var factory = new ChannelFactory<Contracts.IEchoService>(new BasicHttpBinding(), new EndpointAddress(BasicHttpEndPointAddress));
    factory.Open();
    var channel = factory.CreateChannel();
    ((IClientChannel)channel).Open();
    Console.WriteLine("http EchoMessage(\"Complex Hello\") => " + channel.ComplexEcho(new Contracts.EchoMessage() { Text = "Complex Hello" }));
    ((IClientChannel)channel).Close();
    factory.Close();
}
