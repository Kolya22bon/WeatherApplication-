using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherService
{
    private const string ApiKey = "22aca588a03554bed4b832a30c04f333";
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5/";
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponse> GetWeatherAsync(string city)
    {
        var url = $"{BaseUrl}weather?q={city}&units=metric&appid={ApiKey}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Failed to fetch weather data.");
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<WeatherResponse>(content);
    }

    

}
