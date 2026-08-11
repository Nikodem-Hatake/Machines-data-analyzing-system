using System.Configuration;

namespace Domain.Tests
{
    public static class Program
    {
        static void Main(string[] args)
        {
            using(QueueDataConsumer queueDataConsumer = new QueueDataConsumer
            (ConfigurationManager.ConnectionStrings["HostName"].ConnectionString,
            ConfigurationManager.ConnectionStrings["QueueName"].ConnectionString))
            using(DataBaseConnectionManager dataBaseConnectionManager = new DataBaseConnectionManager
            (queueDataConsumer.Channel, ConfigurationManager.ConnectionStrings
            ["DataBaseConnectionString"].ConnectionString))
            {
                if(dataBaseConnectionManager.IsConstructedCorrectly && queueDataConsumer.IsConstructedCorrectly)
                {
                    App app = new App(dataBaseConnectionManager, queueDataConsumer);
                    app.Run();
                }
            }
        }
    }
}