using System.Net.Sockets;

namespace TcpClientDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 13000;
                using TcpClient client = new TcpClient("127.0.0.1", port);
                //server sits on 127.0.0.1
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    Console.WriteLine("Type your message below, follow by [enter]:");
                    string message = Console.ReadLine(); //the message we are sending
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message); // turn the message into bytes

                     //get a stream to server to write to and then read from
                    stream.Write(data, 0, data.Length); //write the message to the network stream
                    Console.WriteLine("Sent: {0}", message);

                    data = new Byte[256]; //prepare for server response byte array buffer
                    String responseData = String.Empty;

                    Int32 bytes = stream.Read(data, 0, data.Length); //read server response up to 256 bytes
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes); //format bytes to ASCII String
                    Console.WriteLine("Received: {0}", responseData);
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
