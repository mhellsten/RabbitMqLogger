using System;
using System.Threading.Tasks;

namespace RabbitMqLogger
{
    public interface ILogger
    {
        Task DebugAsync(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized");
        Task InfoAsync(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized");
        Task WarnAsync(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null);
        Task ErrorAsync(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null);
        Task FatalAsync(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null);
        void Debug(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized");
        void Info(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized");
        void Warn(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null);
        void Error(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null);
        void Fatal(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null);
    }
}
