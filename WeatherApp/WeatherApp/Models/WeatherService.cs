using WeatherApp.Models;
using Newtonsoft.Json;

public class WeatherService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<WeatherInfo?> GetWeatherAsync(string city)
    {
        string apiKey = "your_api_key_here";
        string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string responseContent = await response.Content.ReadAsStringAsync();
        var weatherData = JsonConvert.DeserializeObject<WeatherInfo>(responseContent);

        // Map the response to your WeatherInfo model
        // Example: weatherData.Temperature = parsedData.main.temp;

        return weatherData;
    }
}
