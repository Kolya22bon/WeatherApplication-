public class WeatherResponse
{
    public Main Main { get; set; }
    public Weather[] Weather { get; set; }
    public string Name { get; set; }

    // Добавлено: список прогнозов погоды
    public List<ForecastItem> ForecastList { get; set; }
}

public class Main
{
    public double Temp { get; set; }
    public double Feels_Like { get; set; }
    public double Temp_Min { get; set; }
    public double Temp_Max { get; set; }
}

public class Weather
{
    public string Description { get; set; }
    public string Icon { get; set; }
}

public class ForecastItem
{
    public Main Main { get; set; }
    public Weather[] Weather { get; set; }
    public string Dt_Txt { get; set; } // Дата и время прогноза
}
