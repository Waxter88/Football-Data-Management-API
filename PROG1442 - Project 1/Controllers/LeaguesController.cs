using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG1442___Project_1.Data;
using PROG1442___Project_1.Models;

namespace PROG1442___Project_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly FootballContext _context;

        public LeaguesController(FootballContext context)
        {
            _context = context;
        }

        // GET: api/Leagues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<League>>> GetLeagues()
        {
            return await _context.Leagues
                    .Include(l => l.Teams)
                    .ToListAsync();
        }

        // GET: api/Leagues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<League>> GetLeague(string id)
        {
            var league = await _context.Leagues
                .Include(l => l.Teams)
                .FirstOrDefaultAsync(l => l.ID == id);

            if (league == null)
            {
                return NotFound();
            }

            return league;
        }

        //Get all the Leagues including an integer count of how many Teams are on each or a List of the Teams in the League.
        [HttpGet("count")]
        public async Task<ActionResult<IEnumerable<League>>> GetLeaguesWithCount()
        {
            return await _context.Leagues
                .Include(l => l.Teams)
                .Select(l => new League
                {
                    ID = l.ID,
                    Name = l.Name,
                    TeamCount = l.Teams.Count
                })
                .ToListAsync();
        }

        // PUT: api/Leagues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeague(string id, League league)
        {
            if (id != league.ID)
            {
                return BadRequest();
            }

            _context.Entry(league).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeagueExists(id))
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

        // POST: api/Leagues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<League>> PostLeague(League league)
        {
            _context.Leagues.Add(league);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LeagueExists(league.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLeague", new { id = league.ID }, league);
        }

        // DELETE: api/Leagues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(string id)
        {
            var league = await _context.Leagues.FindAsync(id);
            if (league == null)
            {
                return NotFound();
            }

            _context.Leagues.Remove(league);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeagueExists(string id)
        {
            return _context.Leagues.Any(e => e.ID == id);
        }
    }
}
