using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqLogger
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    public enum MessageType
    {
        Info,
        JobStart,
        JobEnd,
        StepStart,
        StepEnd
    }

    public class LogMessage
    {
        public DateTime DateTimeUtc { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Category { get; set; }
        public string Machine { get; set; }
        public MessageType MessageType { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
