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
                this._connection = new ConnectionFactory()
                {
                    HostName = hostName
                }.CreateConnectionAsync().Result;
                this._channel = this._connection.CreateChannelAsync().Result;
                this._channel.QueueDeclareAsync(queueName, true, false, false).Wait();
            }
            catch(Exception e)
            {
                ExceptionHandler.LogExceptionToConsole(e);
                this.IsConstructedCorrectly = false;
                return;
            }
            this.IsConstructedCorrectly = true;
        }

        public void AddData(string data)
        {
            try
            {
                this._channel.BasicPublishAsync("", this._channel.CurrentQueue, Encoding.UTF8.GetBytes(data));
            }
            catch(Exception e)
            {
                ExceptionHandler.LogExceptionToConsole(e);
            }
        }

        public void Dispose()
        {
            this._channel?.Dispose();
            this._connection?.Dispose();
        }
    }
}
