using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class QueueDataConsumer : IDisposable
    {
        private IChannel? _channel;
        private IConnection? _connection;
        private AsyncEventingBasicConsumer? _consumer;
        public bool IsConstructedSuccesfully { get; }
        public List <string> RecievedMessages { get; }

        public void Dispose()
        {
            this._channel?.Dispose();
            this._connection?.Dispose();
        }

        public QueueDataConsumer(string hostName, string queueName)
        {
            this.RecievedMessages = new List<string>();
            try
            {
                this._connection = new ConnectionFactory()
                {
                    HostName = hostName
                }.CreateConnectionAsync().Result;
                this._channel = this._connection.CreateChannelAsync().Result;
                this._channel.QueueDeclareAsync(queueName, true, false, false);

                this._consumer = new AsyncEventingBasicConsumer(this._channel);
                this._consumer.ReceivedAsync += (obj, eventArgs) =>
                {
                    this.RecievedMessages.Add(Encoding.UTF8.GetString(eventArgs.Body.ToArray()));
                    return null;
                };
                this._channel.BasicConsumeAsync(queueName, true, this._consumer);
            }
            catch(Exception)
            {
                this.IsConstructedSuccesfully = false;
                return;
            }
            this.IsConstructedSuccesfully = true;
        }
    }
}
