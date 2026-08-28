using Domain.Tests.DBContexts;
using Domain.Tests.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Domain.Tests.Controllers
{
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private MachinesDBContext _dataBaseContext;

        public MachinesController(MachinesDBContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        [HttpGet]
        [Route("machine/{id}")]
        public async Task<IActionResult> GetMachine([FromRoute][Required] int? id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string jsonResult = await _dataBaseContext.GetMachineAsync(id.Value, HttpContext.Request.Path);
                if(string.IsNullOrEmpty(jsonResult))
                {
                    return NotFound();
                }
                return Content(jsonResult, "application/json");
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("machines")]
        public async Task<IActionResult> GetMachines()
        {
            try
            {
                return Content(await _dataBaseContext.GetMachinesAsync(HttpContext.Request.Path), "application/json");
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
