using Domain.Tests.Models;
using System.Text.Json;

namespace Domain.Tests.ViewModels.MachineDetails
{
    public class MachineDetailsViewModel
    {
        public const string DATE_TIME_FORMAT_FOR_RECIEVED_DATA = "yyyy-MM-ddTHH:mm";
        public const string DATE_TIME_FORMAT_TO_SEND = "dd-MM-yyyy_HH:mm";

        public int HowManyDatasForward { get; set; }
        public bool IsFormSendedCorrectly { get; set; }
        private int _machineId;
        public string StartDate { get; set; }

        public MachineDetailsViewModel(int machineId)
        {
            _machineId = machineId;
            StartDate = string.Empty;
            HowManyDatasForward = 1;
            IsFormSendedCorrectly = false;
        }

        public MachineDetailsViewModel(int machineId, 
            string startDate, int howManyDatesForward)
        {
            DateTime dateTime = DateTime.ParseExact(startDate,
                DATE_TIME_FORMAT_FOR_RECIEVED_DATA, null);
            if(dateTime.Minute % 10 != 0)
            {
                startDate = dateTime.AddMinutes(-(dateTime.Minute % 10))
                    .ToString(DATE_TIME_FORMAT_FOR_RECIEVED_DATA);
            }

            _machineId = machineId;
            StartDate = startDate;
            HowManyDatasForward = howManyDatesForward;
            IsFormSendedCorrectly = true;
        }

        public async Task<List<AggregatedMachineDatas>> GetAggregatedMachineDatasAsync()
        {
            try
            {
                return JsonSerializer.Deserialize<List<AggregatedMachineDatas>>
                    (await APIConnectionManager.Get($"/machine/{_machineId}" +
                    $"/aggregatedDatas/{DateTime.ParseExact(StartDate.Replace(' ', '_'), 
                    DATE_TIME_FORMAT_FOR_RECIEVED_DATA, null)
                    .ToString(DATE_TIME_FORMAT_TO_SEND)}/{HowManyDatasForward}"),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<AggregatedMachineDatas>();
            }
            catch(Exception)
            {
                return new List<AggregatedMachineDatas>();
            }
        }

        public async Task<Machine?> GetMachineAsync()
        {
            try
            {
                return JsonSerializer.Deserialize<Machine>
                    (await APIConnectionManager.Get($"/machine/{_machineId}"), 
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
