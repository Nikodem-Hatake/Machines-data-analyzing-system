using System;
using System.IO;
using System.Text.Json;

namespace Domain.Tests
{
    public static class ExceptionHandler
    {
        public static void LogExceptionToConsole(Exception e)
        => Console.Write($"{Environment.NewLine}{e.Message}{Environment.NewLine}");
    }
}