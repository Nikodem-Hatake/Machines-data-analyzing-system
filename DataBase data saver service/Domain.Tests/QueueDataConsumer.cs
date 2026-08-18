using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Domain.Tests
{
    public class QueueDataConsumer : IDisposable
    {
        public IChannel Channel { get; private set; }
        private readonly IConnection _connection;
        private readonly AsyncEventingBasicConsumer _consumer;
        public bool IsConstructedCorrectly { get; }

        public QueueDataConsumer(string hostName, string queueName)
        {
            try
            {
                _connection = new ConnectionFactory()
                {
                    HostName = hostName
                }.CreateConnectionAsync().Result;
                Channel = _connection.CreateChannelAsync().Result;
                Channel.QueueDeclareAsync(queueName, true, false, false).Wait();
                _consumer = new AsyncEventingBasicConsumer(Channel);
            }
            catch(Exception e)
            {
                ExceptionHandler.LogExceptionToConsole(e);
                IsConstructedCorrectly = false;
                return;
            }
            IsConstructedCorrectly = true;
        }

        public void AddMethodToInvokeOnRecevingData
        (AsyncEventHandler<BasicDeliverEventArgs> method)
        {
            _consumer.ReceivedAsync += method;
            Channel.BasicConsumeAsync(Channel.CurrentQueue, false, _consumer);
        }

        public void Dispose()
        {
            _connection?.Dispose();
            Channel?.Dispose();
        }
    }
}
