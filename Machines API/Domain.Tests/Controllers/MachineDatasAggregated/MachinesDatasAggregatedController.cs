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

        private DataBaseContext _dataBaseContext;

        public MachinesDatasAggregatedController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        private void AddAggregatedMachineDatasToDataBase
            (AggregatedMachineDatas aggregatedMachineDatas)
        {
            _dataBaseContext.AggregatedMachineDatas.Add(aggregatedMachineDatas);
            _dataBaseContext.SaveChanges();
        }

        [Route("machine/{machineId}/aggregatedDatas/{startDate}/{howManyDatasForward}")]
        public IActionResult GetAggregatedMachineDatas([Required][FromRoute] 
            int? machineId, [DateValidatorAttribute][FromRoute] string? startDate,
            [Required][FromRoute][Range(1, 10)] int? howManyDatasForward)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            startDate = startDate.Replace('_', ' ');
            List<AggregatedMachineDatas> aggregatedMachineDatas 
                = new List<AggregatedMachineDatas>();
            DateTime dateTime = DateTime.ParseExact
                (startDate, DATE_TIME_FORMAT_FOR_PARSED_DATE, null);

            try
            {
                if(_dataBaseContext.Machine.Count(x => x.Id == machineId) == 0)
                {
                    return BadRequest($"Machine with id {machineId} was not found.");
                }

                for(int i = 0; i < howManyDatasForward; ++i)
                {
                    AggregatedMachineDatas? aggregatedMachineData 
                        = GetAggregatedMachineDatasFromDataBase
                            (machineId.Value, dateTime.ToString
                            (DATE_TIME_FORMAT_FOR_PARSED_DATE));
                    if(aggregatedMachineData != null)
                    {
                        aggregatedMachineDatas.Add(aggregatedMachineData);
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

        private AggregatedMachineDatas? GetAggregatedMachineDatasFromDataBase
            (int machineId, string startDate)
        {
            AggregatedMachineDatas? aggregatedMachineDatas = _dataBaseContext
                .AggregatedMachineDatas.FirstOrDefault
                (x => x.StartDate == startDate && x.MachineId == machineId);

            if(aggregatedMachineDatas == null)
            {
                aggregatedMachineDatas = MachineDatasAggregator.Aggregate
                    (_dataBaseContext, machineId, startDate);
                if(aggregatedMachineDatas == null)
                {
                    return null;
                }
                AddAggregatedMachineDatasToDataBase(aggregatedMachineDatas);
            }
            return aggregatedMachineDatas;
        }
    }
}
