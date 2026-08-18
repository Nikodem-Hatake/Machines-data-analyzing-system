namespace Domain.Tests
{
    public class App
    {
        private APIConnectionManager _APIConnectionManager;
        private QueueDataConsumer _queueDataConsumer;

        public App(APIConnectionManager APIConnectionManager, 
            QueueDataConsumer queueDataConsumer)
        {
            _APIConnectionManager = APIConnectionManager;
            _queueDataConsumer = queueDataConsumer;
        }

        public void Run()
        {
            _queueDataConsumer.AddMethodToInvokeOnRecevingData
                (_APIConnectionManager.OnGettingDataFromQueue);
            Console.Read();
        }
    }
}
