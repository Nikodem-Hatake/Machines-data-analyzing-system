using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Domain.Tests
{
    public class APIConnectionManager
    {
        private string _urlWithHostName;

        public APIConnectionManager(string urlWithHostName)
        {
            _urlWithHostName = "http://" + urlWithHostName;
        }

        public async Task<string> Get(string endpoint)
        {
            using(HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient
                    .GetAsync(_urlWithHostName + endpoint);
                if(!response.IsSuccessStatusCode)
                {
                    throw new HttpProtocolException((long)response.StatusCode,
                        string.Empty, null);
                }

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
