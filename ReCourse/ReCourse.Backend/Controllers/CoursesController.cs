using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReCourse.Backend.Data;
using ReCourse.Backend.Models;

namespace ReCourse.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CoursesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<List<Course>>> Get([FromQuery] string? search)
        {
            var q = _db.Courses.Include(c => c.Trainer).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(c => c.Title.Contains(search) || c.Description.Contains(search) || c.Level.Contains(search) || c.Trainer!.FullName.Contains(search));
            return await q.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetById(int id)
        {
            var c = await _db.Courses.Include(x => x.Trainer).FirstOrDefaultAsync(x => x.Id == id);
            if (c == null) return NotFound();
            return c;
        }

        [HttpPost]
        public async Task<ActionResult<Course>> Create(Course model)
        {
            if (model.Trainer != null) { model.TrainerId = model.Trainer.Id; model.Trainer = null; }
            _db.Courses.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Course model)
        {
            if (id != model.Id) return BadRequest();
            _db.Entry(model).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _db.Courses.FindAsync(id);
            if (c == null) return NotFound();
            _db.Courses.Remove(c);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
