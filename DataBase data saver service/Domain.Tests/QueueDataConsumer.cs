using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Domain.Tests
{
    public class QueueDataConsumer : IDisposable
    {
        public IChannel Channel { get; private set; }
        private readonly IConnection _connection;
        private readonly AsyncEventingBasicConsumer _consumer;
        public bool IsConstructedCorrectly { get; }

        public void AddMethodToInvokeOnRecevingData
        (AsyncEventHandler<BasicDeliverEventArgs> method)
        {
            this._consumer.ReceivedAsync += method;
            this.Channel.BasicConsumeAsync(this.Channel.CurrentQueue, false, this._consumer);
        }

        public void Dispose()
        {
            this._connection?.Dispose();
            this.Channel?.Dispose();
        }

        public QueueDataConsumer(string hostName, string queueName)
        {
            try
            {
                this._connection = new ConnectionFactory()
                {
                    HostName = hostName
                }.CreateConnectionAsync().Result;
                this.Channel = this._connection.CreateChannelAsync().Result;
                this.Channel.QueueDeclareAsync(queueName, true, false, false).Wait();
                this._consumer = new AsyncEventingBasicConsumer(this.Channel);
            }
            catch (Exception e)
            {
                ExceptionHandler.LogExceptionToConsole(e);
                this.IsConstructedCorrectly = false;
                return;
            }
            this.IsConstructedCorrectly = true;
        }
    }
}
