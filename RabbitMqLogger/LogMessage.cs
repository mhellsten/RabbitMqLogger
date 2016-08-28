using System;
using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int LogMessageId { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Category { get; set; }
        public string Machine { get; set; }
        public MessageType MessageType { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
