using System;
using System.ServiceModel;

namespace client
{
    class Program
    {
        private readonly static string _basicHttpEndPointAddress = @"http://localhost:8080/basichttp";

        static void Main(string[] args)
        {
            SimpleEcho();
            
            ComplexEcho();
        }

        private static void SimpleEcho()
        {
            var factory = new ChannelFactory<Contracts.IEchoService>(new BasicHttpBinding(), new EndpointAddress(_basicHttpEndPointAddress));
            factory.Open();
            var channel = factory.CreateChannel();
            ((IClientChannel)channel).Open();
            Console.WriteLine("http Echo(\"Hello\") => " + channel.Echo("Hello"));
            ((IClientChannel)channel).Close();
            factory.Close();
        }

        private static void ComplexEcho()
        {
            var factory = new ChannelFactory<Contracts.IEchoService>(new BasicHttpBinding(), new EndpointAddress(_basicHttpEndPointAddress));
            factory.Open();
            var channel = factory.CreateChannel();
            ((IClientChannel)channel).Open();
            Console.WriteLine("http EchoMessage(\"Complex Hello\") => " + channel.ComplexEcho(new Contracts.EchoMessage() { Text = "Complex Hello" }));
            ((IClientChannel)channel).Close();
            factory.Close();
        }
    }
}
