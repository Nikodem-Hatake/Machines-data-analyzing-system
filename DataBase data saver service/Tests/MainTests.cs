using Domain.Tests;
using FluentAssertions;

namespace Tests
{
    public class MainTests
    {
        private const string CORRECT_HOST_NAME = "localhost";
        private const string QUEUE_NAME_FOR_TESTS = "MachinesData.tests";

        [Fact]
        public void QueueDataConsumerConstructsSuccesfully()
        {
            using(QueueDataConsumer queueDataConsumer = new QueueDataConsumer
                (CORRECT_HOST_NAME, QUEUE_NAME_FOR_TESTS))
            {
                queueDataConsumer.IsConstructedCorrectly.Should().BeTrue();
            }
        }
    }
}
