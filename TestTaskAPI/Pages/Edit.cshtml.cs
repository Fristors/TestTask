using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestTaskAPI.Models.Database;

namespace TestTaskAPI.Pages
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;
        [BindProperty]
        public TaskModel Task { get; set; } = new TaskModel();
        public List<StatusModel> Statuses { get; set; } = new List<StatusModel>();

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApi");
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/task/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                Task = JsonSerializer.Deserialize<TaskModel>(json);

                response = await _httpClient.GetAsync($"api/status");

                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();

                    Statuses = JsonSerializer.Deserialize<List<StatusModel>>(json);
                }
                else
                {
                    return RedirectToPage("/Error");
                }

                return Page();
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            var json = JsonSerializer.Serialize(Task);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/task/{Task.id}", content);
            
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
