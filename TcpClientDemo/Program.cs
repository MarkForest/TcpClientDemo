﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientDemo
{
    class Program
    {
        const int port = 8888;
        const string address = "127.0.0.1";

        static void Main(string[] args)
        {
            Console.WriteLine("Введите свое имя: ");
            string userName = Console.ReadLine();
            TcpClient tcpClient = null;
            try
            {
                tcpClient = new TcpClient(address, port);
                NetworkStream stream = tcpClient.GetStream();

                while (true)
                {
                    Console.Write($"{userName}: ");
                    //ввод сообщения
                    string message = Console.ReadLine();
                    message = $"{userName}: {message}";
                    //преобразовываем в масив байт
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    //получаем данные
                    data = new byte[256];
                    StringBuilder stringBuilder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        stringBuilder.Append(Encoding.Unicode.GetString(data));
                    } while (stream.DataAvailable);
                    message = stringBuilder.ToString();
                    Console.WriteLine($"Сервер: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                tcpClient.Close();
            }
        }
    }
}