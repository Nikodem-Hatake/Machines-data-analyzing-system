using Domain.Tests.MachinesDataCollectorSimulation;
using RabbitMQ.Client;

namespace Domain.Tests
{
    public class Program
    {
        private const string HOST_NAME = "rabbitmq";
        private const string QUEUE_NAME = "MachinesData";

        private IMachineDataCollector _machineDataCollector;
        private QueueDataAdder _queueDataAdder;

        static void Main(string[] args)
        {
            using(QueueDataAdder queueDataAdder = new QueueDataAdder(HOST_NAME, QUEUE_NAME))
            {
                if(queueDataAdder.IsConstructedCorrectly)
                {
                    Program program = new Program(new SimulatedMachinesDataCollector(), queueDataAdder);
                    program.Run();
                }
            }
        }

        public Program(IMachineDataCollector machineDataCollector, QueueDataAdder queueDataAdder)
        {
            this._machineDataCollector = machineDataCollector;
            this._queueDataAdder = queueDataAdder;
        }

        public void Run()
        {
            while(true)
            {
                this._machineDataCollector.TryUpdatingMachineData();
                foreach(string machineData in this._machineDataCollector.GetMachinesData())
                {
                    this._queueDataAdder.AddData(machineData);
                }
                Task.Delay(Random.Shared.Next(1500, 2000)).Wait();
            }
        }
    }
}
