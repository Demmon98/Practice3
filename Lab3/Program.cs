using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static int port = 8005; // порт для приема входящих запросов

        // получаем адреса для запуска сокета
        static IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

        // создаем сокет
        static Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static Socket handler1;

        static int ansver;

        static void Main(string[] args)
        {
            RunServer();
        }

        static public void RunServer()
        {
            // связываем сокет с локальной точкой, по которой будем принимать данные
            listenSocket.Bind(ipPoint);

            listenSocket.Listen(10);

            handler1 = listenSocket.Accept();

            if (!handler1.Connected)
                throw new Exception("Не удалось подключится к клиентам");
            else
                Console.WriteLine("Подключено");


            while (true)
            {
                byte[] data = new byte[256];

                handler1.Receive(data);
                ansver = BitConverter.ToInt32(data, 0);

                Model model = new Model();

                switch (ansver)
                {
                    case 1:
                        model.UndergroundLines.Add(new UndergroundLine("newItem", new List<Station>() { new Station() }, 1));
                        model.SaveChanges();

                        handler1.Send(Encoding.Unicode.GetBytes($"New line added!"));
                        Console.WriteLine(1);
                        break;
                    case 2:
                        if (model.UndergroundLines.Count() > 0)
                        {
                            var index = model.UndergroundLines.Count() - 1;
                            var remItem = model.UndergroundLines.ToList()[index];

                            foreach (var item in model.Stations)
                            {
                                model.Stations.Remove(item);
                            }

                            model.UndergroundLines.Remove(remItem);

                            model.SaveChanges();

                            handler1.Send(Encoding.Unicode.GetBytes($"Last line deleted!"));
                        }
                        else
                            handler1.Send(Encoding.Unicode.GetBytes($"Lines empty!"));

                        Console.WriteLine(2);
                        break;
                    case 3:
                        model.Stations.Add(new Station("newItem", 1, 1));
                        model.SaveChanges();

                        handler1.Send(Encoding.Unicode.GetBytes($"New station added!"));

                        Console.WriteLine(3);
                        break;
                    case 4:
                        if (model.Stations.Count() > 0)
                        {
                            var index = model.Stations.Count() - 1;
                            var remItem = model.Stations.ToList()[index];

                            model.Stations.Remove(remItem);

                            model.SaveChanges();

                            handler1.Send(Encoding.Unicode.GetBytes($"Last station deleted!"));
                        }
                        else
                            handler1.Send(Encoding.Unicode.GetBytes($"Stations empty!"));

                        Console.WriteLine(4);
                        break;
                    case 5:
                        if (model.Stations.Count() > 0)
                        {
                            var index = model.Stations.Count() - 1;
                            var lastStation = model.Stations.ToList()[index];

                            lastStation.Name = "changedName";
                            lastStation.SomeProp1 = 2;
                            lastStation.SomeProp2 = 2;

                            model.SaveChanges();

                            handler1.Send(Encoding.Unicode.GetBytes($"Last station changed!"));
                        }
                        else
                            handler1.Send(Encoding.Unicode.GetBytes($"Stations empty!"));

                        Console.WriteLine(5);
                        break;
                    case 6:
                        if (model.Stations.Count() > 0)
                        {
                            var station = model.Stations.First(x => x.Name == "newItem");

                            handler1.Send(Encoding.Unicode.GetBytes(station.ToString()));
                        }
                        else
                            handler1.Send(Encoding.Unicode.GetBytes($"Stations empty!"));

                        Console.WriteLine(6);
                        break;
                    case 7:
                        if (model.UndergroundLines.Count() > 0)
                        {
                            model.Stations.Load();

                            var index = model.UndergroundLines.Count() - 1;
                            var line = model.UndergroundLines.ToList()[index];

                            handler1.Send(Encoding.Unicode.GetBytes(line.Stations.Count.ToString()));
                        }
                        else
                            handler1.Send(Encoding.Unicode.GetBytes($"Lines empty!"));

                        Console.WriteLine(7);
                        break;
                    case 8:
                        if (model.UndergroundLines.Count() > 0)
                        {
                            var index = model.UndergroundLines.Count() - 1;
                            var line = model.UndergroundLines.ToList()[index];

                            string stations = null;

                            foreach (var item in line.Stations)
                            {
                                stations += item.ToString();
                            }

                            handler1.Send(Encoding.Unicode.GetBytes(stations));
                        }
                        else
                            handler1.Send(Encoding.Unicode.GetBytes($"Lines empty!"));

                        Console.WriteLine(8);
                        break;
                    case 9:
                        if (model.UndergroundLines.Count() > 0)
                        {
                            string lines = null;

                            foreach (var item in model.UndergroundLines)
                            {
                                lines += item.ToString();
                            }

                            handler1.Send(Encoding.Unicode.GetBytes(lines));
                        }
                        else
                            handler1.Send(Encoding.Unicode.GetBytes($"Lines empty!"));

                        Console.WriteLine(9);
                        break;
                    default:
                        handler1.Send(Encoding.Unicode.GetBytes($"Invalid operation!"));
                        break;
                }
            }
        }
    }
}
