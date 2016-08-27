﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.FluentConfiguration;
using RabbitMqLogger;

namespace LogWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 || args.Length > 2)
            {
                Usage();
            }

            Console.WriteLine("Watcher is running. Press 'q' to quit.");

            var connection = new ConnectionConfiguration {Hosts = new[] {new HostConfiguration {Host = args[0]}}};
            using (var bus = RabbitHutch.CreateBus(connection, register => { }))
            {
                var configure = args.Length == 1 ? (Action<ISubscriptionConfiguration>)(c => { }) : c => c.WithTopic(args[1]);
                bus.Subscribe<LogMessage>("LogWatcher", PrintMessage, configure);
                while (Console.ReadKey().Key != ConsoleKey.Q) { }
            }
        }

        private static void Usage()
        {
            Console.Error.WriteLine("Usage: \r\n\tLogWatcher <RabbitMQ host name> [subscription]");
            Environment.Exit(1);
        }

        private static void PrintMessage(LogMessage message)
        {
            var previousColor = Console.ForegroundColor;
            var color = 
                message.LogLevel <= LogLevel.Info ? ConsoleColor.White
                : message.LogLevel <= LogLevel.Warn ? ConsoleColor.Yellow 
                : ConsoleColor.Red;
            
            var output = $"{message.DateTimeUtc.ToLocalTime()} [{message.LogLevel}] {message.Machine} {message.MessageType} {message.Category}: {message.Message}";
            if (message.Exception != null)
            {
                output += $"\r\n{message.Exception}";
            }

            Console.ForegroundColor = color;
            Console.WriteLine(output);
            Console.ForegroundColor = previousColor;
        }
    }
}
