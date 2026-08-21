using Domain.Tests.Validators;
using Domain.Tests.ViewModels.MachineDetails;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Domain.Tests.Controllers
{
    public class MachineDetailsController : Controller
    {
        [HttpGet]
        [Route("machineDetails/{machineId}")]
        public IActionResult MachineDetails([Required][FromRoute] int? machineId)
        {
            return View(new MachineDetailsViewModel(machineId ?? 0));
        }

        [Route("getAggregatedMachineDatas/{machineId}/{startDate}/{howManyDatesForward}")]
        public IActionResult MachineDetails([Required][FromRoute] int? machineId,
            [Required][FromRoute][DateValidatorAttribute] string? startDate, 
            [Required][FromRoute] int? howManyDatesForward)
        {
            if(ModelState.IsValid)
            {
                return PartialView("_AggregatedMachineDatasPartialView", 
                    new MachineDetailsViewModel(machineId.Value,
                    startDate, howManyDatesForward.Value));
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("getMachineDetails/{machineId}")]
        public PartialViewResult GetMachineDetails([Required][FromRoute] int? machineId)
        {
            return PartialView("_MachineDetailsPartialView", new
                MachineDetailsViewModel(machineId ?? 0));
        }
    }
}
