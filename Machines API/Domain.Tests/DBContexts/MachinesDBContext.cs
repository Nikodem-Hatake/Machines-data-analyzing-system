using Domain.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Domain.Tests.DBContexts
{
    public class MachinesDBContext : BaseDBContext<Machine>
    {
        public MachinesDBContext(DbContextOptions<MachinesDBContext> options, 
            IDistributedCache distributedCache) : base(options, distributedCache)
        {

        }

        public async Task<bool> ContainsMachine(int id)
        {
            return await _table.CountAsync(x => x.Id == id) == 1;
        }

        public async Task<string> GetMachineAsync(int id, string requestURL)
        {
            string jsonString = await _distributedCache.GetStringAsync(requestURL);
            if(!string.IsNullOrEmpty(jsonString))
            {
                return jsonString;
            }

            jsonString = JsonSerializer.Serialize<Machine>(await _table.FirstOrDefaultAsync(x => x.Id == id));
            await _distributedCache.SetStringAsync(requestURL, jsonString);
            return jsonString;
        }

        public async Task<string> GetMachinesAsync(string requestURL)
        {
            string jsonString = await _distributedCache.GetStringAsync(requestURL);
            if(!string.IsNullOrEmpty(jsonString))
            {
                return jsonString;
            }

            jsonString = JsonSerializer.Serialize<List <Machine>>(await _table.ToListAsync());
            await _distributedCache.SetStringAsync(requestURL, jsonString);
            return jsonString;
        }
    }
}
