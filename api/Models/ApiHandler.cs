using System.Net.Http.Headers;

namespace WeatherForecast.Models;

public class ApiHandler
{
    public required string url { get; set; }
    public ApiHandler() {}

    public HttpResponseMessage SendRequest()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            client.DefaultRequestHeaders.Add(
                "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:137.0) Gecko/20100101 Firefox/137.0"
            );


            HttpRequestMessage request = new(HttpMethod.Get, url);
            HttpResponseMessage response = client.Send(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Success");
                return response;
            }
            else
            {
                Console.WriteLine("internal service error");
                return response;
            }
        }
    }
}