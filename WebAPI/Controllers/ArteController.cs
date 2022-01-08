using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArteController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public ArteController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/Arte
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Arte>>> GetArtes()
        {
            return await _context.Artes.ToListAsync();
        }

        // GET: api/Arte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Arte>> GetArte(int id)
        {
            var arte = await _context.Artes.FindAsync(id);

            if (arte == null)
            {
                return NotFound();
            }

            return arte;
        }

        // GET: api/Arte/mostLiked/tag
        [HttpGet ("mostLiked/{type}")]
        public async Task<ActionResult<string>> GetMostLiked(string type)
        {
            IQueryable<LikeCount> cont;
            if (type.Equals("tag"))
            {
                 cont = _context.Artes.Where(x => x.Like == true).GroupBy(x => x.ArteTag).Select(t => new LikeCount { Key = t.Key, Count = t.Count() });
            }
            else
            {
                cont = _context.Artes.Where(x => x.Like == true).GroupBy(x => x.ArteDate).Select(t => new LikeCount { Key = t.Key, Count = t.Count() });
            }
       
           

            var result = cont.OrderByDescending(x => x.Count).FirstOrDefault();
            return result.Key;    
        }



        // PUT: api/Arte/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArte(int id, Arte arte)
        {
            if (id != arte.ArteId)
            {
                return BadRequest();
            }

            _context.Entry(arte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Arte
        [HttpPost]
        public async Task<ActionResult<Arte>> PostArte(Arte arte)
        {
            _context.Artes.Add(arte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArte", new { id = arte.ArteId }, arte);
        }

        // DELETE: api/Arte/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Arte>> DeleteArte(int id)
        {
            var arte = await _context.Artes.FindAsync(id);
            if (arte == null)
            {
                return NotFound();
            }

            _context.Artes.Remove(arte);
            await _context.SaveChangesAsync();

            return arte;
        }

        private bool ArteExists(int id)
        {
            return _context.Artes.Any(e => e.ArteId == id);
        }
    }
}
