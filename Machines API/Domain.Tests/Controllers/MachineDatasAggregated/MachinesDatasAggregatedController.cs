using Domain.Tests.DBContexts;
using Domain.Tests.Models;
using Domain.Tests.Validators;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Domain.Tests.Controllers.MachineDatasAggregated
{
    [ApiController]
    public class MachinesDatasAggregatedController : ControllerBase
    {
        public const string DATE_TIME_FORMAT = "dd-MM-yyyy_HH:mm";
        public const string DATE_TIME_FORMAT_FOR_PARSED_DATE = "dd-MM-yyyy HH:mm";

        private AggregatedMachinesDatasDBContext _aggregatedMachineDatasDBContext;
        private MachinesDatasDBContext _machinesDatasDBContext;
        private MachinesDBContext _machinesDBContext;

        public MachinesDatasAggregatedController(AggregatedMachinesDatasDBContext aggregatedMachinesDatasDBContext,
            MachinesDBContext machinesDBContext, MachinesDatasDBContext machinesDatasDBContext)
        {
            _aggregatedMachineDatasDBContext = aggregatedMachinesDatasDBContext;
            _machinesDBContext = machinesDBContext;
            _machinesDatasDBContext = machinesDatasDBContext;
        }

        [Route("machine/{machineId}/aggregatedDatas/{startDate}/{howManyDatasForward}")]
        public async Task <IActionResult> GetAggregatedMachineDatas([Required][FromRoute] 
            int? machineId, [DateValidatorAttribute][FromRoute] string startDate,
            [Required][FromRoute][Range(1, 10)] int? howManyDatasForward)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int howManyAggregatedDatasToTake = CalculateHowManyDatasForward(startDate, howManyDatasForward.Value);
            startDate = startDate.Replace('_', ' ');
            List<AggregatedMachineDatas> aggregatedMachineDatas = new();
            DateTime dateTime = DateTime.ParseExact(startDate, DATE_TIME_FORMAT_FOR_PARSED_DATE, null);

            try
            {
                if(!await _machinesDBContext.ContainsMachine(machineId.Value))
                {
                    return BadRequest($"Machine with id {machineId} was not found.");
                }

                for(int i = 0; i < howManyAggregatedDatasToTake; ++i)
                {
                    AggregatedMachineDatas aggregatedMachineDataJsonString
                        = await _aggregatedMachineDatasDBContext.GetAggregatedMachineData
                            (_machinesDatasDBContext, dateTime.ToString(DATE_TIME_FORMAT_FOR_PARSED_DATE),
                            machineId.Value);
                    if(aggregatedMachineDataJsonString != null)
                    {
                        aggregatedMachineDatas.Add(aggregatedMachineDataJsonString);
                    }

                    dateTime = dateTime.AddMinutes(10);
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return new JsonResult(aggregatedMachineDatas);
        }

        private int CalculateHowManyDatasForward
            (string startDate, int howManyDatasForward)
        {
            DateTime dateTime = DateTime.ParseExact
                (startDate, DATE_TIME_FORMAT, null);

            if(dateTime.AddMinutes(10 * howManyDatasForward) < DateTime.Now)
            {
                return howManyDatasForward;
            }
            return howManyDatasForward - (int)(dateTime.AddMinutes
                (10 * (howManyDatasForward + 1)) - DateTime.Now).TotalMinutes / 10;
        }
    }
}
