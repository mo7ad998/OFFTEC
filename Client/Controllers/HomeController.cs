using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Newtonsoft.Json;
using WeatherMVC.Services;
using IdentityModel.Client;

namespace Client.Controllers;

public class HomeController : Controller
{
    private readonly ITokenService _tokenService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ITokenService tokenService,  ILogger<HomeController> logger)
    {
        _logger = logger;
        _tokenService = tokenService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Weather()
    {
        var data = new List<WeatherData>();
        using (var client = new HttpClient())
        {
            var tokenResponse = await _tokenService.GetToken(scope: "api.read");
            client.SetBearerToken(tokenResponse.AccessToken);

            var result = client.GetAsync(requestUri: "https://localhost:7116/WeatherForecast").Result;

            if(result.IsSuccessStatusCode)
            {
                var model = result.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<WeatherData>>(model);

                return View(data);
            }
            else
            {
                throw new Exception("Unable to get content");
            }
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
