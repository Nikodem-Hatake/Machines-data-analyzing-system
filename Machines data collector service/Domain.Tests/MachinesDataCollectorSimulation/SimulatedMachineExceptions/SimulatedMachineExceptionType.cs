using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Tests.MachinesDataCollectorSimulation.SimulatedMachineExceptions
{
    public enum SimulatedMachineExceptionType : byte
    {
        incorrectId,
        stoppedWorking,
        blockedMachine,
        machineOverheat
    }
}
