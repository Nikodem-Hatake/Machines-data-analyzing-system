using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Domain.Tests
{
    public class APIConnectionManager
    {
        private IChannel _channel;
        private string _requestAddress;

        public APIConnectionManager(IChannel channel, string hostName)
        {
            _channel = channel;
            _requestAddress = "http://" + hostName + "/machinedatas";
        }

        public async Task OnGettingDataFromQueue(object sender, BasicDeliverEventArgs eventArgs)
        {
            try
            {
                using(HttpClient httpClient = new HttpClient())
                    await httpClient.PostAsync(_requestAddress, new StringContent
                        (Encoding.UTF8.GetString(eventArgs.Body.ToArray()),
                        Encoding.UTF8, "application/json"));
                await _channel.BasicAckAsync(eventArgs.DeliveryTag, false);
            }
            catch(HttpRequestException e)
            {
                ExceptionHandler.LogExceptionToConsole($"HTTP Exception{Environment.NewLine}" +
                    $"Status code: {e.StatusCode.ToString()}{Environment.NewLine}" +
                    $"Message: {e.Message}");
                await _channel.BasicNackAsync(eventArgs.DeliveryTag, false, true);
            }
            catch(Exception e)
            {
                ExceptionHandler.LogExceptionToConsole(e);
            }
        }
    }
}
