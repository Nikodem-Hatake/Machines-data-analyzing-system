namespace Domain.Tests
{
    public static class ExceptionHandler
    {
        public static void LogExceptionToConsole(Exception e)
        => Console.Write($"{Environment.NewLine}{e.Message}{Environment.NewLine}");
    }
}