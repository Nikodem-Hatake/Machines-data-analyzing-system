namespace Domain.Tests.Controllers.MachineDatasAggregated
{
    public static class MachinesDatasAggregatedControllerValidator
    {
        private static bool ValidateMachineId(string? idAsString)
        {
            int id = 0;
            if(!int.TryParse(idAsString, out id))
            {
                return false;
            }

            return true;
        }

        private static bool ValidateDate(RouteValueDictionary routeValues)
        {
            DateTime dateTime;
            if(!DateTime.TryParseExact(routeValues["startDate"].ToString(), 
                MachinesDatasAggregatedController.DATE_TIME_FORMAT, 
                null, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                return false;
            }

            return dateTime.Minute % 10 == 0 && DateTime.Now >= dateTime.AddMinutes(10);
        }

        public static bool ValidateRouteValues(HttpRequest httpRequest)
        {
            if(!ValidateMachineId(httpRequest.RouteValues["machineId"]?.ToString())
            || !ValidateDate(httpRequest.RouteValues))
            {
                return false;
            }
            return true;
        }
    }
}
