using Domain.Tests.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Domain.Tests.Components.Controls
{
    public partial class AggregatedDataFormComponent
    {
        private AggregatedMachineDatasSelector _aggregatedMachineDatasSelector = new();
        private MudForm _form;

        [Parameter]
        public int MachineId { get; set; }

        [Parameter]
        public EventCallback<string> OnSuccessfullFormSubmitEventCallback { get; set; }
        
        private async Task Submit()
        {
            await _form.ValidateAsync();
            if(!_form.IsValid)
            {
                await dialogService.ShowMessageBoxAsync("Warning", "Form is invalid.");
                return;
            }
            else if(_aggregatedMachineDatasSelector.Time.Value.Minutes % 10 != 0)
            {
                await dialogService.ShowMessageBoxAsync("Warning", "Minutes should end with 0.");
                return;
            }

            await OnSuccessfullFormSubmitEventCallback.InvokeAsync($"machine/{MachineId}/aggregatedDatas/"
                + $"{_aggregatedMachineDatasSelector.StartDate.Value.Add(_aggregatedMachineDatasSelector.Time.Value)
                .ToString("dd-MM-yyyy_HH:mm")}/{_aggregatedMachineDatasSelector.HowManyDatesForward}");
        }
    }
}
