using Microsoft.AspNetCore.Mvc;
using AyaxApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System;

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
        public List<Realtor> Get([FromQuery] RealtorsFilter filter)
        {
            var realtors = _context.Realtors.AsQueryable();
            if (filter.Id != 0)
            {
                realtors = realtors.Where(r => r.Id.Equals(filter.Id));
            }
            if (!string.IsNullOrEmpty(filter.LastName))
            {
                realtors = realtors.Where(r => r.Lastname.ToLowerInvariant().Contains(filter.LastName));
            }
            if (filter.DivisionId != 0)
            {
                realtors = realtors.Where(r => r.DivisionId.Equals(filter.DivisionId));
            }

            realtors = realtors.OrderBy(r => r.Id);
            if (filter.Page != 0)
            {
                return (realtors
                    .Skip((filter.Page - 1) * filter.PageSize)
                    .Take(filter.PageSize).ToList()
                );
            }
            else
            {
                return realtors.ToList();
            }
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
    public class RealtorsFilter
    {
        private int pageSize = 2;
        public long Id { get; set; }
        public string LastName { get; set; }
        public long DivisionId { get; set; }
        public int Page { get; set; }
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value;
        }
    }
}