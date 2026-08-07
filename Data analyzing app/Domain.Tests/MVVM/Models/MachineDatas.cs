using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Tests.MVVM.Models
{
    public class MachineDatas
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int NumberOfProcessedResourcesSinceGettingData { get; set; }
        public double SecondsInWhichResourcesWasProcessed { get; set; }
        public double Temperature { get; set; }
        public string UpdateDataDate { get; set; }
    }
}
