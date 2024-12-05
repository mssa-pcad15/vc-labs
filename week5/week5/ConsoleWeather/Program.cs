using System.Net.Http.Json;

namespace ConsoleWeather
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please provide city name:");
            string city = Console.ReadLine();

            var requestCurrentWeatherUri =
                new Uri(
                    $"https://weatherapi-com.p.rapidapi.com/current.json?q={city}");


            var client = new HttpClient();
            //var request = new HttpRequestMessage(
            //    HttpMethod.Get,
            //    requestCurrentWeatherUri
            //    );
            client.DefaultRequestHeaders.Add("x-rapidapi-key", "c2738f8bc0mshcc0331a25b7d018p1e7966jsne3e1b9d39117");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("x-rapidapi-host", "weatherapi-com.p.rapidapi.com");

            //HttpResponseMessage response = client.SendAsync(request).Result;
            //response.EnsureSuccessStatusCode();

            //string responseBodyInJsonString = response.Content.ReadAsStringAsync().Result;

            //var responseMessage = client.Send(new HttpRequestMessage() { RequestUri = requestCurrentWeatherUri });

            //WeatherAPI result = responseMessage.Content.ReadFromJsonAsync<WeatherAPI>().Result;

            WeatherAPI result = client.GetFromJsonAsync<WeatherAPI>(requestCurrentWeatherUri).Result;
            //Console.WriteLine(responseBodyInJsonString);
            Console.WriteLine($"Currently in {city}, it is {result.current.heatindex_f} degrees F, with humidity {result.current.humidity}% with wind at {result.current.wind_mph}mph");
        }
    }


    public class WeatherAPI
    {
        public Location location { get; set; }
        public Current current { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string tz_id { get; set; }
        public int localtime_epoch { get; set; }
        public string localtime { get; set; }
    }

    public class Current
    {
        public int last_updated_epoch { get; set; }
        public string last_updated { get; set; }
        public float temp_c { get; set; }
        public float temp_f { get; set; }
        public int is_day { get; set; }
        public Condition condition { get; set; }
        public float wind_mph { get; set; }
        public float wind_kph { get; set; }
        public int wind_degree { get; set; }
        public string wind_dir { get; set; }
        public float pressure_mb { get; set; }
        public float pressure_in { get; set; }
        public float precip_mm { get; set; }
        public float precip_in { get; set; }
        public int humidity { get; set; }
        public int cloud { get; set; }
        public float feelslike_c { get; set; }
        public float feelslike_f { get; set; }
        public float windchill_c { get; set; }
        public float windchill_f { get; set; }
        public float heatindex_c { get; set; }
        public float heatindex_f { get; set; }
        public float dewpoint_c { get; set; }
        public float dewpoint_f { get; set; }
        public float vis_km { get; set; }
        public float vis_miles { get; set; }
        public float uv { get; set; }
        public float gust_mph { get; set; }
        public float gust_kph { get; set; }
    }

    public class Condition
    {
        public string text { get; set; }
        public string icon { get; set; }
        public int code { get; set; }
    }


}
