using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8081;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);

            while (true)
            {
                Console.WriteLine("Введите сообщение");
                var message = Console.ReadLine();

                var serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8081);
                udpSocket.SendTo(Encoding.UTF8.GetBytes("Сообщение получено"), serverEndPoint);

                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));

                } while (udpSocket.Available > 0);

                Console.WriteLine(data);
                Console.ReadLine();

            }
        }
    }
}
