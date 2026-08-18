using Domain.Tests.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace Domain.Tests.Controllers
{
    [ApiController]
    public class MachineDatasController : ControllerBase
    {
        private DataBaseContext _dataBaseContext;

        public MachineDatasController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        [HttpPost]
        [Route("machineDatas")]
        public IActionResult AddMachineData()
        {
            try
            {
                AddMachineDataToDataBase(DeserializeMachineData());
            }
            catch(JsonException)
            {
                return BadRequest();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return StatusCode(201);
        }

        private void AddMachineDataToDataBase(MachineDatas machineData)
        {
            _dataBaseContext.MachineDatas.Add(machineData);
            _dataBaseContext.SaveChanges();
        }

        private MachineDatas DeserializeMachineData()
        {
            using(StreamReader streamReader = new StreamReader(HttpContext.Request.Body))
            {
                string requestBody = streamReader.ReadToEndAsync().Result;

                MachineDatas? machineData = JsonSerializer.Deserialize
                    <MachineDatas>(requestBody);
                if(machineData == null)
                {
                    throw new JsonException();
                }
                return machineData;
            }
        }
    }
}
