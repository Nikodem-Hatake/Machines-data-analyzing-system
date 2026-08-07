using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Tests.MVVM.Models
{
    public class MachineDetails
    {
        public double AverageTemperature { get; set; }
        public double AverageTimeProcessingRecources { get; set; }
        public string LastUpdateDateTime { get; set; }
        public int TotalResourcesProcessed { get; set; }
    }
}
