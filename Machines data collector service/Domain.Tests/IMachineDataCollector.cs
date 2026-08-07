using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Tests
{
    public interface IMachineDataCollector
    {
        public IEnumerable<string> GetMachinesData();
        public void TryUpdatingMachineData();
    }
}
