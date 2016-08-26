using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using RabbitMqLogger;

namespace LogWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Watcher is running. Press 'q' to quit.");

            using (var bus = RabbitHutch.CreateBus(new ConnectionConfiguration { Hosts = new[] { new HostConfiguration { Host = args[0] } } }, register => { }))
            { 
                bus.Subscribe<LogMessage>("LogWatcher", PrintMessage, c => c.WithTopic(args.Length == 1 ? "GlobalLogging" : args[1]));
                while (Console.ReadKey().Key != ConsoleKey.Q) { }
            }
        }

        private static void PrintMessage(LogMessage message)
        {
            Console.WriteLine($"{message.DateTimeUtc.ToLocalTime()} {message.LogLevel} {message.Machine} {message.MessageType} {message.Category}: {message.Message} {message.Exception}");
        }
    }
}
