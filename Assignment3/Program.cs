using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    class Program
    {
        private static TcpClient _clientSocket;
        private static Stream _stream;
        private static StreamWriter _writer;
        private static StreamReader _reader;
        static void Main(string[] args)
        {
            Console.WriteLine("Insert port number: ");
            try
            {
                var portNr = Console.ReadLine();
                using (_clientSocket = new TcpClient("127.0.0.1", Convert.ToInt32(portNr)))
                {
                    using (_stream = _clientSocket.GetStream())
                    {
                        _writer = new StreamWriter(_stream)
                        {
                            AutoFlush = true
                        };
                        Console.WriteLine("You are connected to the server");
                        Console.WriteLine();
                        Console.WriteLine("WEIGHT CONVERTER");
                        Console.WriteLine("Type in of the options: \nTOGRAM [number] \nTOOUNCE [number] \nSTOP");
                        while (true)
                        {
                            Console.WriteLine("Type in the message and press enter:");
                            string messageFromClient = Console.ReadLine();
                            _writer.WriteLine(messageFromClient);
                            _reader = new StreamReader(_stream);
                            string messageFromServer = _reader.ReadLine();
                            if (messageFromServer != null)
                            {
                                Console.WriteLine(messageFromServer);
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Disconnected");
                                Console.WriteLine();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Please insert the correct number");
                Console.WriteLine();
            }
        }
    }
}
