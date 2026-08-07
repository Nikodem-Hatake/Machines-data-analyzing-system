using Domain.Tests.MachinesDataCollectorSimulation.SimulatedMachineExceptions;
using System.Text.Json;

namespace Domain.Tests.MachinesDataCollectorSimulation
{
    public class SimulatedMachinesDataCollector : IMachineDataCollector
    {
        private List<SimulatedMachine> _machines;

        public IEnumerable<string> GetMachinesData()
        {
            for(int i = 0; i < this._machines.Count; ++i)
            {
                yield return JsonSerializer.Serialize<SimulatedMachine>(this._machines[i]);
            }
            yield break;
        }

        public SimulatedMachinesDataCollector()
        {
            this._machines = new List<SimulatedMachine>()
            {
                new SimulatedMachine(1, true),
                new SimulatedMachine(2, true),
                new SimulatedMachine(3, true),
                new SimulatedMachine(4, true),
                new SimulatedMachine(5, true),
                new SimulatedMachine(6, false),
                new SimulatedMachine(7, true),
                new SimulatedMachine(8, true),
                new SimulatedMachine(9, false),
                new SimulatedMachine(10, true),
            };
        }

        public void TryUpdatingMachineData()
        {
            foreach(SimulatedMachine machine in this._machines)
            {
                this.TryUpdatingMachineData(machine);
            }
        }

        private void TryUpdatingMachineData(SimulatedMachine machine)
        {
            try
            {
                machine.SimulateUpdatingData();
            }
            catch (Exception e)
            {
                ExceptionHandler.LogExceptionToConsole(e);
            }
        }
    }
}
