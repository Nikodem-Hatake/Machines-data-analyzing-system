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

        [HttpPost]
        [Route("machineDetails/{machineId}")]
        public IActionResult MachineDetails([Required][FromRoute] int? machineId,
            [Required][FromForm][DateValidatorAttribute] string? startDate, 
            [Required][FromForm] int? howManyDatesForward)
        {
            if(ModelState.IsValid)
            {
                return View(new MachineDetailsViewModel(machineId.Value,
                    startDate, howManyDatesForward.Value));
            }
            return View(new MachineDetailsViewModel(machineId ?? 0));
        }
    }
}
