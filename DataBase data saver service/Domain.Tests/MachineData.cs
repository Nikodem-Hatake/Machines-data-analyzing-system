namespace Domain.Tests
{
    public class MachineData
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int NumberOfProcessedResourcesSinceGettingData { get; set; }
        public float SecondsInWhichResourcesWasProcessed { get; set; }
        public float Temperature { get; set; }
        public string UpdateDataDate { get; set; }
    }
}
