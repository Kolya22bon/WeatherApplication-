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

    // Method to get the appropriate weather icon based on the description
    public string GetWeatherIcon(string description)
    {
        if (description.Contains("clear"))
        {
            return "sunny";
        }
        else if (description.Contains("cloud"))
        {
            return "cloudy";
        }
        else if (description.Contains("snow"))
        {
            return "snowy";
        }
        else if (description.Contains("storm"))
        {
            return "stormy";
        }
        else if (description.Contains("moon"))
        {
            return "supermoon";
        }
        else
        {
            return "sunny"; // default icon
        }
    }

    public async Task OnGetAsync(string city)
    {
        if (!string.IsNullOrWhiteSpace(city))
        {
            try
            {
                WeatherData = await _weatherService.GetWeatherAsync(city);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
