using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestTaskAPI.Controllers;
using TestTaskAPI.Models.Database;

namespace TestTaskAPI.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<TaskModel> Tasks { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApi");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _httpClient.GetAsync("api/task");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                Tasks = JsonSerializer.Deserialize<List<TaskModel>>(json);


                return Page();
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
