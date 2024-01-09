using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClientMockDemo
{
    public class NamedayClient
    {
        private HttpClient _client;

        public NamedayClient() : this(new HttpClient()) {}

        public NamedayClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetTodaysNameday()
        {
            var result = await _client.GetAsync(@"https://nameday.abalin.net/api/V1/today?timezone=Europe/Stockholm&country=se");

            // Throw exception if the status code is not OK
            result.EnsureSuccessStatusCode();

            NamedayWrapper nameday = JsonSerializer.Deserialize<NamedayWrapper>(await result.Content.ReadAsByteArrayAsync());

            return nameday.Nameday["se"];
        }
    }
}
