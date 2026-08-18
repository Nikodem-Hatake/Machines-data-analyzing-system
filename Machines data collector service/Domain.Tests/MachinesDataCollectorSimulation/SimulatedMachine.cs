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
            UpdateDataDate = DateTime.Now.ToString(DATETIME_FORMAT);
            MachineId = id;
            IsRunning = isRunning;
        }

        public void SimulateUpdatingData()
        {
            UpdateDataDate = DateTime.Now.ToString(DATETIME_FORMAT);
            NumberOfProcessedResourcesSinceGettingData = Random.Shared.Next(150, 201);
            SecondsInWhichResourcesWasProcessed = Random.Shared.NextSingle() 
                * (float)NumberOfProcessedResourcesSinceGettingData;
            Temperature = (float)Random.Shared.Next(50, 111) + Random.Shared.NextSingle();
        }
    }
}
