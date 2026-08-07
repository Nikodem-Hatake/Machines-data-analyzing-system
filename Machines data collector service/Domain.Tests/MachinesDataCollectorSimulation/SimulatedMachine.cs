using Domain.Tests.MachinesDataCollectorSimulation.SimulatedMachineExceptions;

namespace Domain.Tests.MachinesDataCollectorSimulation
{
    public class SimulatedMachine
    {
        private const string DATETIME_FORMAT = "dd-MM-yyyy HH:mm:ss:fff";

        public int MachineId { get; }
        public bool IsRunning { get; private set; }
        public int NumberOfProcessedResourcesSinceGettingData { get; private set; }
        public float SecondsInWhichResourcesWasProcessed { get; private set; }
        public float Temperature { get; private set; }
        public string UpdateDataDate { get; private set; }

        public SimulatedMachine(int id, bool isRunning)
        {
            if(id < 1)
            {
                throw new SimulateMachineException(this, $"Incorrect id of value {id} was passed.",
                SimulatedMachineExceptionType.incorrectId);
            }
            this.UpdateDataDate = DateTime.Now.ToString(DATETIME_FORMAT);
            this.MachineId = id;
            this.IsRunning = isRunning;
        }

        public void SimulateUpdatingData()
        {
            this.UpdateDataDate = DateTime.Now.ToString(DATETIME_FORMAT);
            this.NumberOfProcessedResourcesSinceGettingData = Random.Shared.Next(150, 201);
            this.SecondsInWhichResourcesWasProcessed = Random.Shared.NextSingle() 
            * (float)this.NumberOfProcessedResourcesSinceGettingData;
            this.Temperature = (float)Random.Shared.Next(50, 111) + Random.Shared.NextSingle();
        }
    }
}
