using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using TestTaskAPI.Models.Database;

namespace TestTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        DataBaseContext _context;
        public TaskController(DataBaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var list = _context.Tasks.Include(u => u.Statuss).ToList();
            string json = JsonSerializer.Serialize(list);
            return Ok(json);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var list = _context.Tasks.Where(u => u.id == id).Include(u => u.Statuss).SingleOrDefault();
            if (list == null)
                return BadRequest("Element is not finded");
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(list, options);
            return Ok(json);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody]TaskModel task)
        {
            try
            {
                task.Statuss = _context.Status.Where(u => u.id == task.Status_ID).SingleOrDefault();
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask([FromBody] TaskModel task)
        {
            if (task == null) return BadRequest("Task is null");
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var _task = await _context.Tasks.Where(u => u.id == id).SingleOrDefaultAsync();
            if (_task == null) return BadRequest("Task is not finded");
            _context.Tasks.Remove(_task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
