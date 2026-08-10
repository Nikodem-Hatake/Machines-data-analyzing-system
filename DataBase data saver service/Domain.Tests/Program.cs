namespace Domain.Tests
{
    public class Program
    {
        private const string HOST_NAME = "rabbitmq";
        private const string QUEUE_NAME = "MachinesData";

        private DataBaseConnectionManager _dataBaseConnectionManager;
        private QueueDataConsumer _queueDataConsumer;

        static void Main(string[] args)
        {
            using(QueueDataConsumer queueDataConsumer = new QueueDataConsumer(HOST_NAME, QUEUE_NAME))
            using(DataBaseConnectionManager dataBaseConnectionManager = new DataBaseConnectionManager
            (queueDataConsumer.Channel, "Server=sqlserver;Database=Machines;User Id=sa;Password=abcd1234._.;TrustServerCertificate=True;"))
            {
                if(dataBaseConnectionManager.IsConstructedCorrectly && queueDataConsumer.IsConstructedCorrectly)
                {
                    Program program = new Program(dataBaseConnectionManager, queueDataConsumer);
                    program.Run();
                }
            }
        }

        public Program(DataBaseConnectionManager dataBaseConnectionManager, QueueDataConsumer queueDataConsumer)
        {
            this._dataBaseConnectionManager = dataBaseConnectionManager;
            this._queueDataConsumer = queueDataConsumer;
        }

        void Run()
        {
            this._queueDataConsumer.AddMethodToInvokeOnRecevingData(this._dataBaseConnectionManager.OnGettingDataFromQueue);
            Console.Read();
        }
    }
}