namespace Domain.Tests
{
    public class App
    {
        private IMachineDataCollector _machineDataCollector;
        private QueueDataAdder _queueDataAdder;

        public App(IMachineDataCollector machineDataCollector, QueueDataAdder queueDataAdder)
        {
            this._machineDataCollector = machineDataCollector;
            this._queueDataAdder = queueDataAdder;
        }

        public void Run()
        {
            while(true)
            {
                this._machineDataCollector.TryUpdatingMachinesData();
                foreach(string machineData in this._machineDataCollector.GetMachinesData())
                {
                    this._queueDataAdder.AddData(machineData);
                }
                Task.Delay(Random.Shared.Next(1500, 2000)).Wait();
            }
        }
    }
}
