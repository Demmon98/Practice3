using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 8005; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера

        static IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static void Main(string[] args)
        {
            Console.WriteLine("Hello! Change what do u want:\n" +
                    "1 - add new line\n" +
                    "2 - delete last line\n" +
                    "3 - add new station\n" +
                    "4 - delete last station\n" +
                    "5 - change last station\n" +
                    "6 - search station on \"newItem\" name\n" +
                    "7 - count of station last line\n" +
                    "8 - list of station last line\n" +
                    "9 - full list of lines\n" +
                    "0 - exit");

            socket.Connect(ipPoint);

            RunClient();
        }

        static void RunClient()
        {
            while (true)
            {
                int answer = int.Parse(Console.ReadLine());

                if (answer == 0)
                    break;

                byte[] data = BitConverter.GetBytes(answer);  // отправляем результат
                socket.Send(data);

                byte[] newData = new byte[512];
                socket.Receive(newData);
                string serverAnsver = Encoding.Unicode.GetString(newData);

                Console.WriteLine(serverAnsver);
            }
        }
    }
}
