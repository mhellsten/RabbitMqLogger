using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;

namespace RabbitMqLogger
{
    public class Logger : ILogger, IDisposable
    {
        private readonly IBus _bus;
        private readonly string _topic;

        public Logger(string host, string topic = "GlobalLogging")
        {
            _bus = RabbitHutch.CreateBus(new ConnectionConfiguration { Hosts = new [] { new HostConfiguration { Host = host } }}, register => {});
            _topic = topic;
        }

        public async Task DebugAsync(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized")
        {
            await PublishMessageAsync(CreateMessage(LogLevel.Debug, message, messageType, category));
        }

        public async Task InfoAsync(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized")
        {
            await PublishMessageAsync(CreateMessage(LogLevel.Info, message, messageType, category));
        }

        public async Task WarnAsync(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null)
        {
            await PublishMessageAsync(CreateMessage(LogLevel.Warn, message, messageType, category, exception));
        }

        public async Task ErrorAsync(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null)
        {
            await PublishMessageAsync(CreateMessage(LogLevel.Error, message, messageType, category, exception));
        }

        public async Task FatalAsync(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null)
        {
            await PublishMessageAsync(CreateMessage(LogLevel.Fatal, message, messageType, category, exception));
        }

        private LogMessage CreateMessage(LogLevel level, string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null)
        {
            return new LogMessage
            {
                LogLevel = level,
                Category = category,
                DateTimeUtc = DateTime.UtcNow,
                Machine = Environment.MachineName,
                Message = message,
                MessageType = messageType,
                Exception = exception?.ToString()
            };
        }

        private void PublishMessage(LogMessage message)
        {
            _bus.Publish(message, _topic);
        }

        private async Task PublishMessageAsync(LogMessage message)
        {
            await _bus.PublishAsync(message, _topic);
        }

        public void Debug(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized")
        {
            DebugAsync(message, messageType, category).Wait();
        }

        public void Info(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized")
        {
            InfoAsync(message, messageType, category).Wait();
        }

        public void Warn(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null)
        {
            WarnAsync(message, messageType, category, exception).Wait();
        }

        public void Error(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null)
        {
            ErrorAsync(message, messageType, category, exception).Wait();
        }

        public void Fatal(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null)
        {
            FatalAsync(message, messageType, category, exception).Wait();
        }

        public void Dispose()
        {
            _bus?.Dispose();
        }
    }
}
