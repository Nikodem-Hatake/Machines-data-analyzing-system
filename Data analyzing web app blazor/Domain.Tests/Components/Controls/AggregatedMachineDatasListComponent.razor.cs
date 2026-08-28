using Domain.Tests.Models;
using Microsoft.AspNetCore.Components;

namespace Domain.Tests.Components.Controls
{
    public partial class AggregatedMachineDatasListComponent
    {
        [Parameter]
        public List<AggregatedMachineDatas> AggregatedMachineDatas { get; set; }

        [Parameter]
        public bool IsLoading { get; set; }
    }
}
