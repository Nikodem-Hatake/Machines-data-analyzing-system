using Domain.Tests.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Domain.Tests.Controllers
{
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private DataBaseContext _dataBaseContext;

        public MachinesController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        [HttpGet]
        [Route("machine/{id}")]
        public IActionResult GetMachine([FromRoute][Required] int? id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return GetMachineFromDataBase(id.Value);
        }

        private IActionResult GetMachineFromDataBase(int id)
        {
            Machine? machine = null;
            try
            {
                machine = _dataBaseContext.Machine.FirstOrDefault(x => x.Id == id);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

            if(machine == null)
            {
                return NotFound();
            }
            return new JsonResult(machine);
        }

        [HttpGet]
        [Route("machines")]
        public IActionResult GetMachines()
        {
            List<Machine>? machines = null;
            try
            {
                machines = _dataBaseContext.Machine.ToList();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return new JsonResult(machines);
        }
    }
}
