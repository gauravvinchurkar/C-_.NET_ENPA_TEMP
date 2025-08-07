using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SampleWeb.Models;

namespace SampleWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, 
                         IHttpClientFactory httpClientFactory,
                         IConfiguration configuration)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetFromJsonAsync<List<WeatherForecast>>("/WeatherForecast");
            return View(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather data");
            ViewBag.Error = "Unable to fetch weather data. Please ensure the API is running.";
            return View(new List<WeatherForecast>());
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; set; }
}
