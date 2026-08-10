namespace Domain.Tests
{
    public interface IMachineDataCollector
    {
        public IEnumerable<string> GetMachinesData();
        public void TryUpdatingMachinesData();
    }
}
