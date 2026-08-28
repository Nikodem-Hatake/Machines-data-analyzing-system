using Domain.Tests.Models;

namespace Domain.Tests.Components.Controls
{
    public partial class MachinesListComponent
    {
        private List<Machine> _machines;

        protected override async Task OnInitializedAsync()
        {
            if(RendererInfo.IsInteractive)
            {
                _machines = await APIConnectionManager.Get<List<Machine>>("machines") ?? new();
            }
            await base.OnInitializedAsync();
        }
    }
}