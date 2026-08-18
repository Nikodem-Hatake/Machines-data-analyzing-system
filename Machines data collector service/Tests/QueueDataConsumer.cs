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
            _channel?.Dispose();
            _connection?.Dispose();
        }

        public QueueDataConsumer(string hostName, string queueName)
        {
            RecievedMessages = new List<string>();
            try
            {
                _connection = new ConnectionFactory()
                {
                    HostName = hostName
                }.CreateConnectionAsync().Result;
                _channel = _connection.CreateChannelAsync().Result;
                _channel.QueueDeclareAsync(queueName, true, false, false);

                _consumer = new AsyncEventingBasicConsumer(_channel);
                _consumer.ReceivedAsync += (obj, eventArgs) =>
                {
                    RecievedMessages.Add(Encoding.UTF8
                        .GetString(eventArgs.Body.ToArray()));
                    return Task.CompletedTask;
                };
                _channel.BasicConsumeAsync(queueName, true, _consumer);
            }
            catch(Exception)
            {
                IsConstructedSuccesfully = false;
                return;
            }
            IsConstructedSuccesfully = true;
        }
    }
}
