using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpEcho
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Connecting to port 8087");

            clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, 8087));
            var stream = new NetworkStream(clientSocket);

            Byte[] data = System.Text.Encoding.ASCII.GetBytes("Hello World Echo");
            await stream.WriteAsync(data, 0, data.Length);

            data = new Byte[256];
            String response = String.Empty;
            // Read the Tcp Server Response Bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

            Console.WriteLine("Received: {0}", response);
        }
    }
}