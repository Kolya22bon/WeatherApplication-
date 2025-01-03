using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly WeatherService _weatherService;

    public IndexModel(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public WeatherResponse WeatherData { get; private set; }
    public string ErrorMessage { get; private set; }

    // Метод для получения подходящей иконки погоды на основе описания
    public string GetWeatherIcon(string description)
    {
        if (description.Contains("clear", StringComparison.OrdinalIgnoreCase))
        {
            return "sunny";
        }
        else if (description.Contains("cloud", StringComparison.OrdinalIgnoreCase))
        {
            return "cloudy";
        }
        else if (description.Contains("snow", StringComparison.OrdinalIgnoreCase))
        {
            return "snowy";
        }
        else if (description.Contains("storm", StringComparison.OrdinalIgnoreCase))
        {
            return "stormy";
        }
        else if (description.Contains("moon", StringComparison.OrdinalIgnoreCase))
        {
            return "supermoon";
        }
        else
        {
            return "sunny"; // Иконка по умолчанию
        }
    }

    public async Task OnGetAsync(string city)
    {
        if (!string.IsNullOrWhiteSpace(city))
        {
            try
            {
                // Получаем текущую погоду
                WeatherData = await _weatherService.GetWeatherAsync(city);

                // Получаем прогноз и добавляем его в WeatherData
                var forecastData = await _weatherService.GetForecastAsync(city);
                WeatherData.ForecastList = forecastData.ForecastList;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
