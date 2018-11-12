using Microsoft.AspNetCore.Mvc;
using AyaxApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace AyaxApi.Controllers
{
    [Route("api/[controller]")]
    public class DivisionsController : ControllerBase
    {
        private Context _context;
        public DivisionsController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Division> Get()
        {
            return _context.Divisions.ToList();
        }
        [HttpGet("{Id}", Name = "GetDivision")]
        public IActionResult Get(long id)
        {
            var item = _context.Divisions.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Division item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.Divisions.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetDivision", new { id = item.Id }, item);
        }
        [HttpPut("{Id}")]
        public IActionResult Update(long id, [FromBody] Division item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }
            var division = _context.Divisions.Find(id);
            if (division == null)
            {
                return NotFound();
            }
            division.Name = item.Name;
            _context.Update(division);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _context.Divisions.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Divisions.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }
}