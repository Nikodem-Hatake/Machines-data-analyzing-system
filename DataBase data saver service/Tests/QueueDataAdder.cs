using RabbitMQ.Client;
using System.Text;

namespace Tests
{
    public class QueueDataAdder : IDisposable
    {
        private readonly string _queueName;
        private readonly IChannel? _channel;
        private readonly IConnection? _connection;
        public bool IsConstructedCorrectly { get; }

        public void AddData(string data) => this._channel.BasicPublishAsync("", this._queueName, Encoding.UTF8.GetBytes(data));

        public void Dispose()
        {
            this._channel?.Dispose();
            this._connection?.Dispose();
        }

        public QueueDataAdder(string hostName, string queueName)
        {
            this._queueName = queueName;
            try
            {
                this._connection = new ConnectionFactory()
                {
                    HostName = hostName
                }.CreateConnectionAsync().Result;
                this._channel = this._connection.CreateChannelAsync().Result;
                this._channel.QueueDeclareAsync(this._queueName, true, false, false).Wait();
            }
            catch(Exception e)
            {
                this.IsConstructedCorrectly = false;
                return;
            }
            this.IsConstructedCorrectly = true;
        }
    }
}
