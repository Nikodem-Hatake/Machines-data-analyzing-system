using Domain.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Domain.Tests.DBContexts
{
    public class MachinesDatasDBContext : BaseDBContext<MachineDatas>
    {
        public MachinesDatasDBContext(DbContextOptions<MachinesDatasDBContext> options, 
            IDistributedCache distributedCache) : base(options, distributedCache)
        {

        }

        public void Add(MachineDatas machineDatas)
        {
            _table.AddAsync(machineDatas);
            SaveChangesAsync();
        }

        public IQueryable<MachineDatas> GetMachineDatas(int machineId, string startDate, string endDate)
        {
            return _table.Where(x => x.MachineId == machineId
                && x.UpdateDataDate.CompareTo(startDate) > -1
                && x.UpdateDataDate.CompareTo(endDate) < 0);
        }
    }
}
