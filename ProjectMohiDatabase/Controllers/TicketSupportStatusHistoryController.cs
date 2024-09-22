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
    public class TicketSupportStatusHistoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public TicketSupportStatusHistoriesController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // GET: api/TicketSupportStatusHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketSupportStatusHistoryDTOs>>> GetTicketSupportStatusHistories()
        {
            return await _context.TicketSupportStatusHistories
                .Select(history => new TicketSupportStatusHistoryDTOs
                {
                    TicketSupportID = history.TicketSupportID,
                    StatusID = history.StatusID,
                    StatusName = history.TicketStatus.StatusName,
                    UpdatedAt = history.UpdatedAt
                }).ToListAsync();
        }

        // GET: api/TicketSupportStatusHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TicketSupportStatusHistoryDTOs>>> GetTicketSupportStatusHistory(int id)
        {
            var statusHistories = new List<TicketSupportStatusHistoryDTOs>();
            string connectionString = _configuration.GetConnectionString("con");

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetTicketSupportStatusHistory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TicketSupportID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var historyDTO = new TicketSupportStatusHistoryDTOs
                            {
                                TicketSupportID = reader.GetInt32(reader.GetOrdinal("TicketSupportID")),
                                StatusID = reader.GetInt32(reader.GetOrdinal("StatusID")),
                                StatusName = reader.GetString(reader.GetOrdinal("StatusName")),
                                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                            };
                            statusHistories.Add(historyDTO);
                        }
                    }
                }
            }

            if (statusHistories.Count == 0)
            {
                return NotFound(); // Return 404 if no records found
            }

            return Ok(statusHistories); // Return the list of DTOs
        }
        // POST: api/TicketSupportStatusHistories
        [HttpPost]
        public async Task<ActionResult<TicketSupportStatusHistory>> PostTicketSupportStatusHistory(TicketSupportStatusHistoryCreateDTO createDTO)
        {
            var statusHistory = new TicketSupportStatusHistory
            {
                TicketSupportID = createDTO.TicketSupportID,
                StatusID = createDTO.StatusID,
                UpdatedAt = createDTO.UpdatedAt
            };

            _context.TicketSupportStatusHistories.Add(statusHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketSupportStatusHistory", new { id = statusHistory.TicketSupportID }, statusHistory);
        }

        // DELETE: api/TicketSupportStatusHistories/5
        [HttpDelete("{id}/{updatedAt}")]
        public async Task<IActionResult> DeleteTicketSupportStatusHistory(int id, DateTime updatedAt)
        {
            var statusHistory = await _context.TicketSupportStatusHistories
                .FirstOrDefaultAsync(h => h.TicketSupportID == id && h.UpdatedAt == updatedAt);

            if (statusHistory == null)
            {
                return NotFound();
            }

            _context.TicketSupportStatusHistories.Remove(statusHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketSupportStatusHistoryExists(int id, DateTime updatedAt)
        {
            return _context.TicketSupportStatusHistories
                .Any(h => h.TicketSupportID == id && h.UpdatedAt == updatedAt);
        }
    }
}
