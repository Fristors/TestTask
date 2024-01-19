using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TestTaskAPI.Models.Database;

namespace TestTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        DataBaseContext _context;
        public StatusController(DataBaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            var list = _context.Status.ToList();
            string json = JsonSerializer.Serialize(list);
            return Ok(json);
        }
    }
}
