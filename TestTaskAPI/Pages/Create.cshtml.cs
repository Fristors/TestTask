using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestTaskAPI.Models.Database;

namespace TestTaskAPI.Pages
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public TaskModel Task { get; set; } = new TaskModel();

        public List<StatusModel> Statuses { get; set; } = new List<StatusModel>();

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApi");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _httpClient.GetAsync($"api/status");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                Statuses = JsonSerializer.Deserialize<List<StatusModel>>(json);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Task.Statuss = new StatusModel();

            var json = JsonSerializer.Serialize(Task);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/task", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
