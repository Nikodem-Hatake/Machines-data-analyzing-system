namespace Domain.Tests
{
    public static class APIConnectionManager
    {
        public static string? APIUrl { get; set; }

        public static async Task<string> Get(string endPoint)
        {
            using(HttpClient httpClient = new HttpClient())
            return await httpClient.GetStringAsync($"{APIUrl}{endPoint}");
        }
    }
}
