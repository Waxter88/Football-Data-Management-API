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
    public class PlayersController : ControllerBase
    {
        private readonly FootballContext _context;

        public PlayersController(FootballContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync();

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        //Get all the Players including an integer count of how many Teams each player is on or a List of the Teams.
        [HttpGet("team")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayersWithTeam()
        {
            var players = await _context.Players
                .Include(p => p.Team)
                .Select(p => new Player
                {
                    ID = p.ID,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Team = p.Team
                })
                .ToListAsync();
            return players;
        }
        //Get the above data filtered for one Team
        [HttpGet("team/{id}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayersWithTeam(int id)
        {
            var players = await _context.Players
                .Include(p => p.Team)
                .Where(p => p.Team.ID == id)
                .Select(p => new Player
                {
                    ID = p.ID,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Team = p.Team
                })
                .ToListAsync();
            return players;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.ID)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.ID }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.ID == id);
        }
    }
}
