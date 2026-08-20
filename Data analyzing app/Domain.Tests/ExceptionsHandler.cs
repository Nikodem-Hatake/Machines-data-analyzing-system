using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Tests
{
    public static class ExceptionsHandler
    {
        public static void LogExceptionToAlertAsync(string message)
            => Shell.Current.DisplayAlertAsync("Error", message, "cancel");

        public static void LogHTTPExceptionToAlertAsync(HttpProtocolException e)
        {
            Shell.Current.DisplayAlertAsync("Error", $"Http error. Status code: " +
                $"{e.ErrorCode.ToString()}{Environment.NewLine}Message: " +
                $"{e.Message}", "cancel");
        }
    }
}
