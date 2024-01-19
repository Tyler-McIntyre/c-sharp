using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

public class WeatherController : Controller
{
    private readonly WeatherService _weatherService = new WeatherService();

    public IActionResult Index()
    {
        return View(new WeatherInfo());
    }

    [HttpPost]
    public async Task<IActionResult> Index(string city)
    {
        var weatherInfo = await _weatherService.GetWeatherAsync(city);
        return View(weatherInfo);
    }
}
