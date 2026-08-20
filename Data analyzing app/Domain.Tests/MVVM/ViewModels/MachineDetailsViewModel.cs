using Domain.Tests.MVVM.Models;
using Microsoft.IdentityModel.Tokens;
using PropertyChanged;
using System.Text.Json;

namespace Domain.Tests.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MachineDetailsViewModel
    {
        public List<AggregatedMachineDatas> AggregatedMachineDatas { get; set; }
        private APIConnectionManager _APIConnectionManager;
        public Command GetAggregatedMachineDatasCommand { get; private set;  }
        public int HowManyToTakeForwardMinusOne { get; set; }
        public bool IsAggregatedMachineDatasLoaded { get; set; }
        public Machine Machine { get; set; }
        public DateTime StartDateTime { get; set; }
        public TimeSpan Time { get; set; }

        public MachineDetailsViewModel(APIConnectionManager 
            APIConnectionManager, Machine machine)
        {
            AggregatedMachineDatas = new List<AggregatedMachineDatas>();
            _APIConnectionManager = APIConnectionManager;
            IsAggregatedMachineDatasLoaded = true;
            Machine = machine;
            StartDateTime = DateTime.Now.Date;
            CreateCommands();
        }

        private void CreateCommands()
        {
            GetAggregatedMachineDatasCommand = new Command(async () =>
            {
                if(!IsAggregatedMachineDatasLoaded)
                {
                    return;
                }

                IsAggregatedMachineDatasLoaded = false;
                if(Time.Minutes % 10 != 0)
                {
                    Time -= TimeSpan.FromMinutes(Time.Minutes % 10);
                }
                await GetAggregatedMachineDatas();
                IsAggregatedMachineDatasLoaded = true;
            });
        }

        private async Task GetAggregatedMachineDatas()
        {
            List<AggregatedMachineDatas> aggregatedMachineDatas;

            try
            {
                aggregatedMachineDatas 
                    = await GetAggregatedMachineDatasFromAPI();
            }
            catch(HttpProtocolException e)
            {
                ExceptionsHandler.LogHTTPExceptionToAlertAsync(e);
                return;
            }
            catch(Exception e)
            {
                ExceptionsHandler.LogExceptionToAlertAsync
                    ($"Error occured when getting aggregated machine datas: " +
                    $"{e.Message}");
                return;
            }

            AggregatedMachineDatas = aggregatedMachineDatas;
        }

        private async Task<List <AggregatedMachineDatas>> GetAggregatedMachineDatasFromAPI()
        {
            string jsonString = await _APIConnectionManager.Get
                ($"/machine/{Machine.Id}/aggregatedDatas/" +
                $"{StartDateTime.Add(Time).ToString("dd-MM-yyyy_HH:mm")}/" +
                $"{HowManyToTakeForwardMinusOne + 1}");

            if(!jsonString.IsNullOrEmpty())
            {
                return JsonSerializer.Deserialize
                    <List <AggregatedMachineDatas>>(jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? new List<AggregatedMachineDatas>();
            }
            return new List<AggregatedMachineDatas>();
        }
    }
}
