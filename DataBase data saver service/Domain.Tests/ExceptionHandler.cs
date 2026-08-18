namespace Domain.Tests
{
    public static class ExceptionHandler
    {
        public static void LogExceptionToConsole(Exception e)
            => Console.Write($"{Environment.NewLine}{e.Message}{Environment.NewLine}");

        public static void LogExceptionToConsole(string message)
            => Console.Write($"{Environment.NewLine}{message}{Environment.NewLine}");
    }
}