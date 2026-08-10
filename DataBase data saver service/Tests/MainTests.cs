using Domain.Tests;
using FluentAssertions;
using System.Diagnostics;
using System.Reflection;

namespace Tests
{
    public class MainTests
    {
        private const string CORRECT_CONNECTION_STRING = "Server=localhost,1433;Database=Machines.Tests;User Id=sa;Password=abcd1234._.;TrustServerCertificate=True;";
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

        [Theory]
        [InlineData("{\"MachineId\":1,\"IsRunning\":true,\"NumberOfProcessedResourcesSinceGettingData\":160,\"SecondsInWhichResourcesWasProcessed\":67.43153,\"Temperature\":98.73897,\"UpdateDataDate\":\"10-08-2026 06:33:03:243\"}")]
        [InlineData("{\"MachineId\":2,\"IsRunning\":true,\"NumberOfProcessedResourcesSinceGettingData\":194,\"SecondsInWhichResourcesWasProcessed\":114.83931,\"Temperature\":93.45554,\"UpdateDataDate\":\"10-08-2026 06:33:03:243\"}")]
        [InlineData("{\"MachineId\":3,\"IsRunning\":true,\"NumberOfProcessedResourcesSinceGettingData\":182,\"SecondsInWhichResourcesWasProcessed\":50.593266,\"Temperature\":77.25892,\"UpdateDataDate\":\"10-08-2026 06:33:03:243\"}")]
        public void DataBaseConnectionManagerAddsDataFromQueueSuccesfully(string data)
        {
            using(QueueDataConsumer queueDataConsumer = new QueueDataConsumer(CORRECT_HOST_NAME, QUEUE_NAME_FOR_TESTS))
            using(QueueDataAdder queueDataAdder = new QueueDataAdder(CORRECT_HOST_NAME, QUEUE_NAME_FOR_TESTS))
            using(DataBaseConnectionManager dataBaseConnectionManager = new DataBaseConnectionManager
            (queueDataConsumer.Channel, CORRECT_CONNECTION_STRING))
            {
                MachinesDatasDBContext machinesDatasDBContext = dataBaseConnectionManager.GetType()
                .GetField("_machinesDatasDBContext", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(dataBaseConnectionManager) as MachinesDatasDBContext;
                int latestId = machinesDatasDBContext.MachineDatas.Max(x => x.Id);

                queueDataConsumer.AddMethodToInvokeOnRecevingData(dataBaseConnectionManager.OnGettingDataFromQueue);
                queueDataAdder.AddData(data);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while (stopwatch.Elapsed.TotalSeconds < 0.5)
                {

                }

                machinesDatasDBContext.MachineDatas.Max(x => x.Id).Should().BeGreaterThan(latestId);
            }
        }
    }
}
