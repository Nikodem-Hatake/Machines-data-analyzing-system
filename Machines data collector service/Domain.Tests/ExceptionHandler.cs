using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Domain.Tests
{
    public static class ExceptionHandler
    {
        public static void LogExceptionToConsole(Exception e)
            => Console.WriteLine($"{Environment.NewLine}{e.Message}{Environment.NewLine}");
    }
}
