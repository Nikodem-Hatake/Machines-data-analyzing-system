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
            List<AggregatedMachineDatas> aggregatedMachineDatas 
                = new List<AggregatedMachineDatas>();
            Exception? exception = null;

            for(int i = 0; i <= HowManyToTakeForwardMinusOne; ++i)
            {
                try
                {
                    AggregatedMachineDatas? aggregatedMachineData
                        = await GetAggregatedMachineData(i);
                    if(aggregatedMachineData != null)
                    {
                        aggregatedMachineDatas.Add(aggregatedMachineData);
                    }
                }
                catch(Exception e)
                {
                    exception = e;
                }
            }

            LogExceptionToExceptionHandlerForGettingAggregatedMachineDatas(exception);
            AggregatedMachineDatas = aggregatedMachineDatas;
        }

        private async Task<AggregatedMachineDatas?> GetAggregatedMachineData
            (int currentTimeStartNumber)
        {
            string jsonString = await _APIConnectionManager.Get
                ($"/machine/{Machine.Id}/aggregatedDatas/" +
                $"{StartDateTime.Add(Time).AddMinutes(currentTimeStartNumber * 10)
                .ToString("dd-MM-yyyy_HH:mm")}");

            if(!jsonString.IsNullOrEmpty())
            {
                return JsonSerializer.Deserialize
                    <AggregatedMachineDatas>(jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? null;
            }
            return null;
        }

        private void LogExceptionToExceptionHandlerForGettingAggregatedMachineDatas
            (Exception? exception)
        {
            if(exception is HttpProtocolException e)
            {
                ExceptionsHandler.LogExceptionToAlertAsync
                    ($"Http error occured. Status code: {e.ErrorCode}");
            }
            else if(exception != null)
            {
                ExceptionsHandler.LogExceptionToAlertAsync
                    ($"Error occured when getting aggregated machine datas: " +
                    $"{exception.Message}");
            }
        }
    }
}
