namespace Domain.Tests
{
    public class App
    {
        private DataBaseConnectionManager _dataBaseConnectionManager;
        private QueueDataConsumer _queueDataConsumer;

        public App(DataBaseConnectionManager dataBaseConnectionManager, QueueDataConsumer queueDataConsumer)
        {
            this._dataBaseConnectionManager = dataBaseConnectionManager;
            this._queueDataConsumer = queueDataConsumer;
        }

        public void Run()
        {
            this._queueDataConsumer.AddMethodToInvokeOnRecevingData
            (this._dataBaseConnectionManager.OnGettingDataFromQueue);
            Console.Read();
        }
    }
}
