using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Domain.Tests
{
    public class DataBaseConnectionManager : IDisposable
    {
        private readonly IChannel _channel;
        public bool IsConstructedCorrectly { get; }
        private readonly MachinesDatasDBContext? _machinesDatasDBContext;

        public DataBaseConnectionManager(IChannel channel, string connectionString)
        {
            this._channel = channel;
            try
            {
                this._machinesDatasDBContext = new MachinesDatasDBContext(connectionString);
            }
            catch(Exception e)
            {
                ExceptionHandler.LogExceptionToConsole(e);
                this.IsConstructedCorrectly = false;
                return;
            }
            this.IsConstructedCorrectly = true;
        }

        public void Dispose() => this._machinesDatasDBContext?.Dispose();

        public async Task OnGettingDataFromQueue(object sender, BasicDeliverEventArgs eventArgs)
        {
            try
            {
                MachineData machineDatas = JsonSerializer.Deserialize<MachineData>
                (Encoding.UTF8.GetString(eventArgs.Body.ToArray())) 
                ?? throw new Exception("Couldn't convert json to MachineData");

                this._machinesDatasDBContext.MachineDatas.Add(machineDatas);
                this._machinesDatasDBContext.SaveChanges();
                await this._channel.BasicAckAsync(eventArgs.DeliveryTag, false);
            }
            catch(Exception e)
            {
                ExceptionHandler.LogExceptionToConsole(e);
                await this._channel.BasicNackAsync(eventArgs.DeliveryTag, false, true);
            }
        }
    }
}
