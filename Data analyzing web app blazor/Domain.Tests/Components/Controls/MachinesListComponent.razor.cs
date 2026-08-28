using Domain.Tests.Models;

namespace Domain.Tests.Components.Controls
{
    public partial class MachinesListComponent
    {
        private List<Machine> _machines;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                _machines = await APIConnectionManager.Get<List<Machine>>("machines") ?? new();
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}