using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Text.Json;
using System.Web.Mvc;

namespace ConsoleApp21
{
    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string Response { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string title, string year, string type)
        {
            var client = _httpClientFactory.CreateClient();
            var apiKey = "YOUR_OMDB_API_KEY";
            var url = $"http://www.omdbapi.com/?apikey={apiKey}&t={title}&y={year}&type={type}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var movie = JsonSerializer.Deserialize<Movie>(content);

                return View(movie);
            }
            else
            {
                return View();
            }
        }
    }
}
