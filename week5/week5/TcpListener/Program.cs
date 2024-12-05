using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpListenerDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                //loopback address 127.0.0.1

                server = new TcpListener(localAddr, port);
                server.Start();
                Byte[] bytes = new Byte[256];
                String data = null;

                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    using TcpClient client = server.AcceptTcpClient();
                    //this will block the execution until a client is connected
                    Console.WriteLine("Connected!");
                    data = null;
                    NetworkStream stream = client.GetStream();
                    int i;
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }
                }
             }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
