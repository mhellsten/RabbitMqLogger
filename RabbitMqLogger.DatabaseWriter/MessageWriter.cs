using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;

namespace RabbitMqLogger.DatabaseWriter
{
    class MessageWriter
    {
        private readonly LogDbContext _dbContext;
        private readonly SemaphoreSlim _semaphore;
        
        public MessageWriter(IBus messageBus, LogDbContext dbContext)
        {
            _semaphore = new SemaphoreSlim(1);
            _dbContext = dbContext;
            messageBus.SubscribeAsync<LogMessage>("DatabaseWriter", WriteMessage);
        }

        public async Task WriteMessage(LogMessage message)
        {
            await _semaphore.WaitAsync();
            try
            {
                _dbContext.LogMessages.Add(message);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine($"Wrote {message.LogLevel} message to database");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("An error occurred when saving message to database: " + ex);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
