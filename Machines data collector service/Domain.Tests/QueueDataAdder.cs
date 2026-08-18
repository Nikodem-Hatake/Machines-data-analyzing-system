using RabbitMQ.Client;
using System.Text;

namespace Domain.Tests
{
    public class QueueDataAdder : IDisposable
    {
        private readonly IChannel? _channel;
        private readonly IConnection? _connection;
        public bool IsConstructedCorrectly { get; }

        public QueueDataAdder(string hostName, string queueName)
        {
            try
            {
                _connection = new ConnectionFactory()
                {
                    HostName = hostName
                }.CreateConnectionAsync().Result;
                _channel = _connection.CreateChannelAsync().Result;
                _channel.QueueDeclareAsync(queueName, true, false, false).Wait();
            }
            catch(Exception e)
            {
                ExceptionHandler.LogExceptionToConsole(e);
                IsConstructedCorrectly = false;
                return;
            }
            IsConstructedCorrectly = true;
        }

        public void AddData(string data)
        {
            try
            {
                _channel.BasicPublishAsync("", _channel.CurrentQueue, 
                    Encoding.UTF8.GetBytes(data));
            }
            catch(Exception e)
            {
                ExceptionHandler.LogExceptionToConsole(e);
            }
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
