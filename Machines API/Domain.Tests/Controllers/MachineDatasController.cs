using Domain.Tests.DBContexts;
using Domain.Tests.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Domain.Tests.Controllers
{
    [ApiController]
    public class MachineDatasController : ControllerBase
    {
        private MachinesDatasDBContext _dataBaseContext;

        public MachineDatasController(MachinesDatasDBContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        [HttpPost]
        [Route("machineDatas")]
        public IActionResult AddMachineData([FromBody][Required] 
            MachineDatas machineDatas)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _dataBaseContext.Add(machineDatas);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return StatusCode(201);
        }
    }
}
