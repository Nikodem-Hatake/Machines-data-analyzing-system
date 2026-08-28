using Domain.Tests.Models;
using Microsoft.AspNetCore.Components;

namespace Domain.Tests.Components.Controls
{
    public partial class MachineComponent
    {
        [Parameter]
        public Machine Machine { get; set; }

        private void NavigateToDetailsPage() => navigationManager.NavigateTo($"/details/{Machine.Id}");
    }
}
