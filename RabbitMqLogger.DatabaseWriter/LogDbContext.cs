using System.Data.Entity;

namespace RabbitMqLogger.DatabaseWriter
{
    class LogDbContext : DbContext
    {
        public DbSet<LogMessage> LogMessages { get; set; }

        public LogDbContext(string connectionString) : base(connectionString)
        {            
        }
    }
}
