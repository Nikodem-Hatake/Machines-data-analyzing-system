using Domain.Tests.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Tests.Controllers.MachineDatasAggregated
{
    [ApiController]
    public class MachinesDatasAggregatedController : ControllerBase
    {
        public const string DATE_TIME_FORMAT = "dd-MM-yyyy_HH:mm";

        private DataBaseContext _dataBaseContext;

        public MachinesDatasAggregatedController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        [Route("machine/{machineId}/aggregatedDatas/{startDate}")]
        public IActionResult GetAggregatedMachineDatas()
        {
            if(!MachinesDatasAggregatedControllerValidator
                .ValidateRouteValues(HttpContext.Request))
            {
                return BadRequest();
            }

            AggregatedMachineDatas? aggregatedMachineDatas;
            try
            {
                aggregatedMachineDatas = GetAggregatedMachineDatasFromDataBase();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

            if(aggregatedMachineDatas == null)
            {
                return Content("", "text/plain");
            }
            return new JsonResult(aggregatedMachineDatas);
        }

        private AggregatedMachineDatas? GetAggregatedMachineDatasFromDataBase()
        {
            int machineId = int.Parse(HttpContext.Request.RouteValues["machineId"].ToString());
            string startDate = HttpContext.Request.RouteValues["startDate"]
                .ToString().Replace('_', ' ');

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
                _dataBaseContext.AggregatedMachineDatas.Add(aggregatedMachineDatas);
                _dataBaseContext.SaveChanges();
            }
            return aggregatedMachineDatas;
        }
    }
}
