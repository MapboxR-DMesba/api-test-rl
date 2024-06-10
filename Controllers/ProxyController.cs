using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_test_rl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("reverse/{longitude},{latitude}")]
        public async Task<IActionResult> GetReverseGeocode(double longitude, double latitude)
        {
            //-79.43813293000855,43.66436644418484
            //longitude = -79.43813293000855;
            //latitude = 43.66436644418484;
//var url = $"https://api.mapbox.com/search/v1/reverse/{longitude},{latitude}?access_token=pk.eyJ1IjoibWVzYmF1bGhhc2FuIiwiYSI6ImNsd3pjMGc5cjA1bHIyanFwOGlib3JtNmMifQ.BYcuMdFSAA3bjnTVN9LGEw&language=en&limit=1&types=address";
var url = $"https://api.mapbox.com/search/v1/reverse/{longitude},{latitude}?access_token=pk.eyJ1IjoibWVzYmF1bGhhc2FuIiwiYSI6ImNsd3pjMGc5cjA1bHIyanFwOGlib3JtNmMifQ.BYcuMdFSAA3bjnTVN9LGEw&language=en&limit=1&types=locality";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Origin", "https://labs.mapbox.com"); // Set your custom origin here

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }
    }
}
