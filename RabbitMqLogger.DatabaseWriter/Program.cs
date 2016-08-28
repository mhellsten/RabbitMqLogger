using System;
using System.ServiceProcess;

namespace RabbitMqLogger.DatabaseWriter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var service = new Service1();

            if (!Environment.UserInteractive)
            {
                ServiceBase.Run(service);
            }
            else
            {
                service.Start();

                Console.WriteLine("Running on console. Press 'q' to quit.");
                while (Console.ReadKey().Key != ConsoleKey.Q) { }
                Console.WriteLine("Stopping service");

                service.Stop();
            }
        }
    }
}
