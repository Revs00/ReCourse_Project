using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReCourse.Backend.Data;
using ReCourse.Backend.Models;

namespace ReCourse.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TrainersController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<List<Trainer>>> Get([FromQuery] string? search)
        {
            var q = _db.Trainers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(t => t.FullName.Contains(search) || t.Expertise.Contains(search) || t.Email.Contains(search));
            return await q.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainer>> GetById(int id)
        {
            var t = await _db.Trainers.FindAsync(id);
            if (t == null) return NotFound();
            return t;
        }

        [HttpPost]
        public async Task<ActionResult<Trainer>> Create(Trainer model)
        {
            _db.Trainers.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Trainer model)
        {
            if (id != model.Id) return BadRequest();
            _db.Entry(model).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var t = await _db.Trainers.FindAsync(id);
            if (t == null) return NotFound();
            _db.Trainers.Remove(t);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
