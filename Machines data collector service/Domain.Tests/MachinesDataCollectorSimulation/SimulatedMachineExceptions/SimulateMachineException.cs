namespace Domain.Tests.MachinesDataCollectorSimulation.SimulatedMachineExceptions
{
    public class SimulateMachineException : Exception
    {
        public SimulatedMachineExceptionType SimulatedMachineExceptionType { get; }
        public SimulatedMachine SimulatedMachineThatThrownException { get; }

        public SimulateMachineException(SimulatedMachine simulatedMachineThatThrownException, 
            string message, SimulatedMachineExceptionType simulatedMachineExceptionType 
            = SimulatedMachineExceptionType.stoppedWorking)
            : base(message)
        {
            SimulatedMachineThatThrownException = simulatedMachineThatThrownException;
            SimulatedMachineExceptionType = simulatedMachineExceptionType;
        }
    }
}
