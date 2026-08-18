namespace Domain.Tests
{
    public class App
    {
        private IMachineDataCollector _machineDataCollector;
        private QueueDataAdder _queueDataAdder;

        public App(IMachineDataCollector machineDataCollector, QueueDataAdder queueDataAdder)
        {
            _machineDataCollector = machineDataCollector;
            _queueDataAdder = queueDataAdder;
        }

        public void Run()
        {
            while(true)
            {
                _machineDataCollector.TryUpdatingMachinesData();
                foreach(string machineData in _machineDataCollector.GetMachinesData())
                {
                    _queueDataAdder.AddData(machineData);
                }
                Task.Delay(Random.Shared.Next(1500, 2000)).Wait();
            }
        }
    }
}
