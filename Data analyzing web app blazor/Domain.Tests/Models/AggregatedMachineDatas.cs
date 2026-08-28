namespace Domain.Tests.Models
{
    public class AggregatedMachineDatas
    {
        public double AverageSecondsInWhichResourceIsProcessed { get; set; }
        public double AverageTemperature { get; set; }
        public int Id { get; set; }
        public int MachineId { get; set; }
        public double MaximumTemperature { get; set; }
        public double MinimumTemperature { get; set; }
        public string StartDate { get; set; }
        public int TotalNumberOfProcessedResources { get; set; }
    }
}
