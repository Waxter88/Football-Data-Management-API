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
    public class TeamsController : ControllerBase
    {
        private readonly FootballContext _context;

        public TeamsController(FootballContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams
                .Include(t => t.League)
                .Include(t => t.Players)
                .FirstOrDefaultAsync();

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        //Get all the Teams including the Name of the league the team is on (no player data)
        [HttpGet("league")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsWithLeague()
        {
            var teams = await _context.Teams
                .Include(t => t.League)
                .Select(t => new Team
                {
                    ID = t.ID,
                    Name = t.Name,
                    League = t.League
                })
                .ToListAsync();
            if(teams == null)
            {
                return NotFound();
            }
            return teams;
        }
        //Get the above data but also include an integer count of how many Players are on each or a List of the Players on the Team.
        [HttpGet("league/count")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsWithLeagueAndCount()
        {
            var teams = await _context.Teams
                .Include(t => t.League)
                .Include(t => t.Players)
                .Select(t => new Team
                {
                    ID = t.ID,
                    Name = t.Name,
                    League = t.League,
                    PlayerCount = t.Players.Count
                })
                .ToListAsync();
            return teams;
        }
        //Get the above data filtered for one League
        [HttpGet("league/{id}")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsWithLeagueAndCount(int id)
        {
            return await _context.Teams
                .Include(t => t.League)
                .Include(t => t.Players)
                .Where(t => t.League.ID == id.ToString())
                .Select(t => new Team
                {
                    ID = t.ID,
                    Name = t.Name,
                    League = t.League,
                    PlayerCount = t.Players.Count
                })
                .ToListAsync();
        }


        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.ID)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.ID }, team);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.ID == id);
        }
    }
}
