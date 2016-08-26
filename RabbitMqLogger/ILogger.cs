using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqLogger
{
    public interface ILogger
    {
        void Debug(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized");
        void Info(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized");
        void Warn(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null);
        void Error(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null);
        void Fatal(string message, MessageType messageType = MessageType.Info, string category = "Uncategorized", Exception exception = null);
    }
}
