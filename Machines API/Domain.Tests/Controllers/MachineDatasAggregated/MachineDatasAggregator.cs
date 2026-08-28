using Domain.Tests.DBContexts;
using Domain.Tests.Models;
using System.Reflection.PortableExecutable;

namespace Domain.Tests.Controllers.MachineDatasAggregated
{
    public static class MachineDatasAggregator
    {
        public static AggregatedMachineDatas? Aggregate(MachinesDatasDBContext dataBaseContext,
            int machineId, string startDate)
        {
            IQueryable<MachineDatas> machineDatas = GetMachineDatasFromDataBase
                (dataBaseContext, machineId, startDate);

            if(machineDatas.Count() == 0)
            {
                return null;
            }

            AggregatedMachineDatas aggregatedMachineDatas = new AggregatedMachineDatas
            {
                AverageSecondsInWhichResourceIsProcessed = machineDatas
                    .Sum(x => x.SecondsInWhichResourcesWasProcessed),
                AverageTemperature = machineDatas.Average(x => x.Temperature),
                MachineId = machineId,
                MaximumTemperature = machineDatas.Max(x => x.Temperature),
                MinimumTemperature = machineDatas.Min(x => x.Temperature),
                StartDate = startDate,
                TotalNumberOfProcessedResources = machineDatas
                    .Sum(x => x.NumberOfProcessedResourcesSinceGettingData)
            };
            aggregatedMachineDatas.AverageSecondsInWhichResourceIsProcessed /= (double)
                aggregatedMachineDatas.TotalNumberOfProcessedResources;
            return aggregatedMachineDatas;
        }

        private static IQueryable<MachineDatas> GetMachineDatasFromDataBase
            (MachinesDatasDBContext dataBaseContext, int machineId, string startDate)
        {
            DateTime endDate = DateTime.ParseExact(startDate,
                MachinesDatasAggregatedController.DATE_TIME_FORMAT_FOR_PARSED_DATE, null)
                .AddMinutes(10);
            string endDateString = endDate.ToString
                (MachinesDatasAggregatedController.DATE_TIME_FORMAT_FOR_PARSED_DATE);

            return dataBaseContext.GetMachineDatas(machineId, startDate, endDateString);
        }
    }
}
