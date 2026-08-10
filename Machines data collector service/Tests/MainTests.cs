using Domain.Tests;
using Domain.Tests.MachinesDataCollectorSimulation;
using Domain.Tests.MachinesDataCollectorSimulation.SimulatedMachineExceptions;
using FluentAssertions;
using System.Diagnostics;

namespace Tests
{
    public class MainTests
    {
        private const string CORRECT_HOST_NAME = "localhost";
        private const string QUEUE_NAME_FOR_TESTS = "MachinesData.tests";

        [Theory]
        [InlineData(1, false)]
        [InlineData(0, true)]
        [InlineData(-1, true)]
        [InlineData(-10, true)]
        [InlineData(int.MinValue, true)]
        [InlineData(int.MaxValue, false)]
        public void SimulatedMachineConstructorThrowsExceptionWhenWrongIdIsPassed(int id, bool shouldThrowException)
        {
            try
            {
                SimulatedMachine simulatedMachine = new SimulatedMachine(id, false);
                shouldThrowException.Should().BeFalse();
            }
            catch (Exception e)
            {
                e.Should().BeOfType<SimulateMachineException>().Which.SimulatedMachineExceptionType.Should().Be
                (SimulatedMachineExceptionType.incorrectId);
                shouldThrowException.Should().BeTrue();
            }
        }

        [Theory]
        [InlineData(CORRECT_HOST_NAME, true)]
        [InlineData("fakeOnet.pl", false)]
        [InlineData("0.0.0.0", false)]
        public void QueueDataAdderConstructionExpectsToBePassedBoolValue(string hostName, bool shouldConstructCorrectly)
        {
            using(QueueDataAdder queueDataAdder = new QueueDataAdder(hostName, QUEUE_NAME_FOR_TESTS))
            {
                queueDataAdder.IsConstructedCorrectly.Should().Be(shouldConstructCorrectly);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("Hello world")]
        [InlineData("{}")]
        public void QueueDataAdderAddsDataSuccesfully(string data)
        {
            using(QueueDataAdder queueDataAdder = new QueueDataAdder(CORRECT_HOST_NAME, QUEUE_NAME_FOR_TESTS))
            {
                queueDataAdder.AddData(data);
            }
            using(QueueDataConsumer queueDataConsumer = new QueueDataConsumer(CORRECT_HOST_NAME, QUEUE_NAME_FOR_TESTS))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while(stopwatch.Elapsed.TotalSeconds < 0.5)
                {

                }
                queueDataConsumer.RecievedMessages.Should().Contain(data);
            }
        }
    }
}
