using Microsoft.AspNetCore.Mvc;
using AyaxApi.Models;
using System.Collections.Generic;
using System.Linq;
using System;

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
        public List<Division> Get([FromQuery] DivisionsFilter filter)
        {
            var divisions = _context.Divisions.AsQueryable();
            if (filter.Id != 0)
            {
                divisions = divisions.Where(r => r.Id.Equals(filter.Id));
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                divisions = divisions.Where(r => r.Name.ToLowerInvariant().Contains(filter.Name));
            }

            divisions = divisions.OrderBy(r => r.Id);
            if (filter.Page != 0)
            {
                return (divisions
                    .Skip((filter.Page - 1) * filter.PageSize)
                    .Take(filter.PageSize).ToList()
                );
            }
            else
            {
                return divisions.ToList();
            }
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
        /// <summary>
        /// Creates a Division item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/divisions
        ///     {
        ///        "id": 1,
        ///        "name": "name",
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created Division item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>      
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
        /// <summary>
        /// Deletes a specific Division item.
        /// </summary>
        /// <param name="id"></param>        
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
    public class DivisionsFilter
    {
        private int pageSize = 2;
        public long Id { get; set; }
        public string Name { get; set; }
        public int Page { get; set; }
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value;
        }
    }
}
