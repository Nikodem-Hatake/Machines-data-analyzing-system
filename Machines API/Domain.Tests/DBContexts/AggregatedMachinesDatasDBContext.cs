using Domain.Tests.Controllers.MachineDatasAggregated;
using Domain.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Domain.Tests.DBContexts
{
    public class AggregatedMachinesDatasDBContext : BaseDBContext<AggregatedMachineDatas>
    {
        public AggregatedMachinesDatasDBContext(DbContextOptions<AggregatedMachinesDatasDBContext> options, 
            IDistributedCache distributedCache) : base(options, distributedCache)
        {

        }

        public async Task<AggregatedMachineDatas> GetAggregatedMachineData(MachinesDatasDBContext machinesDatasDBContext,
            string startDate, int machineId)
        {
            string cacheKey = $"aggregatedMachineDatas/{machineId}/{startDate}";
            string jsonString = await _distributedCache.GetStringAsync(cacheKey);
            if(!string.IsNullOrWhiteSpace(jsonString))
            {
                return JsonSerializer.Deserialize<AggregatedMachineDatas>(jsonString);
            }

            AggregatedMachineDatas aggregatedMachineDatas = await _table.FirstOrDefaultAsync
                (x => x.StartDate == startDate && x.MachineId == machineId);
            if(aggregatedMachineDatas == null)
            {
                aggregatedMachineDatas = MachineDatasAggregator.Aggregate
                   (machinesDatasDBContext, machineId, startDate);
                if(aggregatedMachineDatas == null)
                {
                    return null;
                }
                Add(aggregatedMachineDatas);
            }
            
            jsonString = JsonSerializer.Serialize<AggregatedMachineDatas>(aggregatedMachineDatas);
            await _distributedCache.SetStringAsync(cacheKey, jsonString);
            return aggregatedMachineDatas;
        }

        public void Add(AggregatedMachineDatas aggregatedMachineDatas)
        {
            _table.Add(aggregatedMachineDatas);
            SaveChanges();
        }
    }
}
