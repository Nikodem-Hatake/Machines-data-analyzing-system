using Domain.Tests.MachinesDataCollectorSimulation;
using System.Configuration;

namespace Domain.Tests
{
    public static class Program
    {
        static void Main(string[] args)
        {
            using(QueueDataAdder queueDataAdder = new QueueDataAdder
                (ConfigurationManager.ConnectionStrings["HostName"].ConnectionString,
                ConfigurationManager.ConnectionStrings["QueueName"].ConnectionString))

            if(queueDataAdder.IsConstructedCorrectly)
            {
                App app = new App(new SimulatedMachinesDataCollector(), queueDataAdder);
                app.Run();
            }
        }
    }
}
