using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Tests
{
    public static class ExceptionsHandler
    {
        public static void LogExceptionToAlertAsync(string message)
            => Shell.Current.DisplayAlertAsync("Błąd", message, "cancel");
    }
}
