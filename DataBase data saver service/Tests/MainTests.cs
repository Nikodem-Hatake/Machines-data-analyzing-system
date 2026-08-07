using Domain.Tests;
using FluentAssertions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Tests
{
    public class MainTests
    {
        private const string CORRECT_CONNECTION_STRING = "Data Source=DESKTOP-3IF4V5L\\SQLEXPRESS;Initial Catalog=Machines;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True";
        private const string CORRECT_HOST_NAME = "localhost";
        private const string QUEUE_NAME_FOR_TESTS = "MachinesData.tests";

        [Fact]
        public void DataBaseConnectionManagerConstructsSuccesfully()
        {
            using (QueueDataConsumer queueDataConsumer = new QueueDataConsumer(CORRECT_HOST_NAME, QUEUE_NAME_FOR_TESTS))
            using (DataBaseConnectionManager dataBaseConnectionManager = new DataBaseConnectionManager
            (queueDataConsumer.Channel, CORRECT_CONNECTION_STRING))
            {
                dataBaseConnectionManager.IsConstructedCorrectly.Should().BeTrue();
            }
        }

        [Fact]
        public void QueueDataConsumerConstructsSuccesfully()
        {
            using(QueueDataConsumer queueDataConsumer = new QueueDataConsumer(CORRECT_HOST_NAME, QUEUE_NAME_FOR_TESTS))
            {
                queueDataConsumer.IsConstructedCorrectly.Should().BeTrue();
            }
        }
    }
}
