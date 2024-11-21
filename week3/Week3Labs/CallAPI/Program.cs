using System.Text.Json.Serialization;

namespace CallAPI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://usa-lottery-result-all-state-api.p.rapidapi.com/lottery-results/game-result?gameID=24&drawID=98114"),
                Headers =
    {
        { "x-rapidapi-key", "c2738f8bc0mshcc0331a25b7d018p1e7966jsne3e1b9d39117" },
        { "x-rapidapi-host", "usa-lottery-result-all-state-api.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
               
                Console.WriteLine(body);
            }
        }
    }
}