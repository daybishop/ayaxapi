using Microsoft.AspNetCore.Mvc;
using AyaxApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace AyaxApi.Controllers
{
    [Route("api/realtors")]
    [Authorize]
    public class RealtorsController : ControllerBase
    {
        private Context _context;
        public RealtorsController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Realtor> Get()
        {
            return _context.Realtors.ToList();
        }
        [HttpGet("{id}", Name = "GetRealtor")]
        public IActionResult Get(long id)
        {
            var item = _context.Realtors.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Realtor item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.Realtors.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetRealtor", new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Realtor item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }
            var realtor = _context.Realtors.Find(id);
            if (realtor == null)
            {
                return NotFound();
            }
            realtor.Firstname = item.Firstname;
            realtor.Lastname = item.Lastname;
            realtor.DivisionId = item.DivisionId;
            _context.Update(realtor);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _context.Realtors.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Realtors.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }
}