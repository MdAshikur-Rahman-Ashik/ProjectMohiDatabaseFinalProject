using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectMohiDatabase.Models.DAL;
using ProjectMohiDatabase.Models;
using Microsoft.EntityFrameworkCore;
using ProjectMohiDatabase.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ProjectMohiDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketSupportStatusHistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketSupportStatusHistoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TicketSupportStatusHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketSupportStatusHistoryDTOs>>> GetTicketSupportStatusHistories()
        {
            var histories = await _context.TicketSupportStatusHistories
                .Include(th => th.TicketStatus) // Include related TicketStatus to get StatusName
                .Select(th => new TicketSupportStatusHistoryDTOs
                {
                    TicketSupportStatusHistoryID = th.TicketSupportStatusHistoryID,
                    TicketSupportID = th.TicketSupportID,
                    StatusID = th.StatusID,
                    StatusName = th.TicketStatus.StatusName, // Assuming TicketStatus has a Name property
                    UpdatedAt = th.UpdatedAt
                })
                .ToListAsync();

            return Ok(histories);
        }

        // GET: api/TicketSupportStatusHistory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketSupportStatusHistoryDTOs>> GetTicketSupportStatusHistory(int id)
        {
            var history = await _context.TicketSupportStatusHistories
                .Include(th => th.TicketStatus)
                .Where(th => th.TicketSupportStatusHistoryID == id)
                .Select(th => new TicketSupportStatusHistoryDTOs
                {
                    TicketSupportStatusHistoryID = th.TicketSupportStatusHistoryID,
                    TicketSupportID = th.TicketSupportID,
                    StatusID = th.StatusID,
                    StatusName = th.TicketStatus.StatusName,
                    UpdatedAt = th.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        // POST: api/TicketSupportStatusHistory
        [HttpPost]
        public async Task<ActionResult<TicketSupportStatusHistoryDTOs>> PostTicketSupportStatusHistory(TicketSupportStatusHistoryCreateDTO createDto)
        {
            var ticketSupportStatusHistory = new TicketSupportStatusHistory
            {
                TicketSupportID = createDto.TicketSupportID,
                StatusID = createDto.StatusID,
                UpdatedAt = createDto.UpdatedAt
            };

            _context.TicketSupportStatusHistories.Add(ticketSupportStatusHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicketSupportStatusHistory), new { id = ticketSupportStatusHistory.TicketSupportStatusHistoryID }, createDto);
        }

        // DELETE: api/TicketSupportStatusHistory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketSupportStatusHistory(int id)
        {
            var history = await _context.TicketSupportStatusHistories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            _context.TicketSupportStatusHistories.Remove(history);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content on successful deletion
        }
    }
}
