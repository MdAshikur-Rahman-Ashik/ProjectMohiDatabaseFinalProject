using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMohiDatabase.Models.DAL;
using ProjectMohiDatabase.Models;
using ProjectMohiDatabase.Models.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace ProjectMohiDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketManagementsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public TicketManagementsController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }




        // GET: api/TicketManagements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketManagementDTOs>>> GetTicketManagements()
        {
            return await _context.TicketManagements
                .Select(tm => new TicketManagementDTOs
                {
                    TicketManagementID = tm.TicketManagementID,
                    TicketSupportID = tm.TicketSupportID,
                    AssignedTo = tm.AssignedTo,
                    ManagedByPersonID = tm.ManagedByPersonID
                }).ToListAsync();
        }

        // GET: api/TicketManagements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketManagementDTOs>> GetTicketManagement(int id)
        {
            var ticketManagementDTO = new TicketManagementDTOs();

            // Fetch connection string from configuration
            string connectionString = _configuration.GetConnectionString("con");

            using (var connection = new SqlConnection(connectionString))  // Pass actual connection string
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetTicketManagementByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TicketManagementID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            return NotFound();
                        }

                        if (await reader.ReadAsync())
                        {
                            ticketManagementDTO.TicketManagementID = reader.GetInt32(reader.GetOrdinal("TicketManagementID"));
                            ticketManagementDTO.TicketSupportID = reader.GetInt32(reader.GetOrdinal("TicketSupportID"));
                            ticketManagementDTO.AssignedTo = reader.GetString(reader.GetOrdinal("AssignedTo"));
                            ticketManagementDTO.ManagedByPersonID = reader.GetInt32(reader.GetOrdinal("ManagedByPersonID"));
                        }
                    }
                }
            }

            return Ok(ticketManagementDTO);
        }


        // POST: api/TicketManagements
        [HttpPost]
        public async Task<ActionResult<TicketManagement>> PostTicketManagement([FromForm]TicketManagementCreateDTO ticketManagementCreateDTO)
        {
            var ticketManagement = new TicketManagement
            {
                TicketSupportID = ticketManagementCreateDTO.TicketSupportID,
                AssignedTo = ticketManagementCreateDTO.AssignedTo,
                ManagedByPersonID = ticketManagementCreateDTO.ManagedByPersonID
            };

            _context.TicketManagements.Add(ticketManagement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketManagement", new { id = ticketManagement.TicketManagementID }, ticketManagement);
        }

        // PUT: api/TicketManagements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketManagement(int id, [FromForm] TicketManagementCreateDTO ticketManagementCreateDTO)
        {
            if (id != ticketManagementCreateDTO.TicketSupportID)
            {
                return BadRequest();
            }

            var ticketManagement = await _context.TicketManagements.FindAsync(id);

            if (ticketManagement == null)
            {
                return NotFound();
            }

            ticketManagement.AssignedTo = ticketManagementCreateDTO.AssignedTo;
            ticketManagement.ManagedByPersonID = ticketManagementCreateDTO.ManagedByPersonID;

            _context.Entry(ticketManagement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketManagementExists(id))
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

        // DELETE: api/TicketManagements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketManagement(int id)
        {
            var ticketManagement = await _context.TicketManagements.FindAsync(id);
            if (ticketManagement == null)
            {
                return NotFound();
            }

            _context.TicketManagements.Remove(ticketManagement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketManagementExists(int id)
        {
            return _context.TicketManagements.Any(e => e.TicketManagementID == id);
        }
    }
}
