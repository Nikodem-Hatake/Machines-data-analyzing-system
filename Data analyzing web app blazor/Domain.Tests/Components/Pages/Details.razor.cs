using Domain.Tests.Models;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Domain.Tests.Components.Pages
{
    public partial class Details
    {
        private List<AggregatedMachineDatas> _aggregatedMachineDatas = new();

        [Parameter]
        [Required]
        public int? Id { get; set; }

        private bool _isLoading;

        protected override void OnInitialized()
        {
            if(Id == null)
            {
                navigationManager.NavigateTo("/");
            }
        }

        private async void OnSuccessfullFormSubmit(string endpoint)
        {
            if(_isLoading)
            {
                return;
            }

            _aggregatedMachineDatas.Clear();
            _isLoading = true;
            _aggregatedMachineDatas = await APIConnectionManager.Get<List<AggregatedMachineDatas>>(endpoint) ?? new();
            _isLoading = false;
            StateHasChanged();
        }
    }
}