using Domain.Tests.Models;
using System.Reflection.PortableExecutable;

namespace Domain.Tests.Controllers.MachineDatasAggregated
{
    public static class MachineDatasAggregator
    {
        private const string DATE_TIME_FORMAT_FOR_PARSED_DATE = "dd-MM-yyyy HH:mm";

        public static AggregatedMachineDatas? Aggregate(DataBaseContext dataBaseContext,
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
            (DataBaseContext dataBaseContext, int machineId, string startDate)
        {
            DateTime endDate = DateTime.ParseExact(startDate,
                DATE_TIME_FORMAT_FOR_PARSED_DATE, null);
            endDate = endDate.AddMinutes(10);
            string endDateString = endDate.ToString
                (DATE_TIME_FORMAT_FOR_PARSED_DATE);

            return dataBaseContext.MachineDatas
                .Where(x => x.MachineId == machineId 
                && x.UpdateDataDate.CompareTo(startDate) > -1
                && x.UpdateDataDate.CompareTo(endDateString) < 0);
        }
    }
}
