using Domain.Tests.Models;
using System.Text.Json;

namespace Domain.Tests.ViewModels.Home
{
    public class HomeViewModel
    {
        public async Task <List<Machine>?> GetMachinesAsync()
        {
            try
            {   
                return JsonSerializer.Deserialize<List<Machine>>
                    (await APIConnectionManager.Get("/machines"),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
