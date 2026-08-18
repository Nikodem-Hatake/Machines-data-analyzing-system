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

            if(queueDataConsumer.IsConstructedCorrectly )
            {
                App app = new App(new APIConnectionManager(queueDataConsumer.Channel,
                    ConfigurationManager.ConnectionStrings["APIHostName"].ConnectionString), 
                    queueDataConsumer);
                app.Run();
            }
        }
    }
}