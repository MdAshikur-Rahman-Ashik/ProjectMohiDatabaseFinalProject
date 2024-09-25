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
                    ManagedByApplicationUserID = tm.ManagedByApplicationUserID
                }).ToListAsync();
        }

        // GET: api/TicketManagements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketManagementDTOs>> GetTicketManagement(int id)
        {
            // Fetch the TicketManagement data from the database
            var ticketManagement = await _context.TicketManagements
                .FirstOrDefaultAsync(tm => tm.TicketManagementID == id);

            if (ticketManagement == null)
            {
                return NotFound($"Ticket Management with ID {id} not found.");
            }

            // Map the entity to the DTO
            var ticketManagementDTO = new TicketManagementDTOs
            {
                TicketManagementID = ticketManagement.TicketManagementID,
                TicketSupportID = ticketManagement.TicketSupportID,
                AssignedTo = ticketManagement.AssignedTo,
                ManagedByApplicationUserID = ticketManagement.ManagedByApplicationUserID
            };

            return Ok(ticketManagementDTO);
        }



        // POST: api/TicketManagements
        [HttpPost]
        public async Task<ActionResult<TicketManagement>> PostTicketManagement([FromBody] TicketManagementCreateDTO ticketManagementCreateDTO)
        {
            var ticketManagement = new TicketManagement
            {
                TicketSupportID = ticketManagementCreateDTO.TicketSupportID,
                AssignedTo = ticketManagementCreateDTO.AssignedTo,
                ManagedByApplicationUserID = ticketManagementCreateDTO.ManagedByApplicationUserID
            };

            _context.TicketManagements.Add(ticketManagement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketManagement", new { id = ticketManagement.TicketManagementID }, ticketManagement);
        }

        // PUT: api/TicketManagements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketManagement(int id, [FromBody] TicketManagementCreateDTO ticketManagementCreateDTO)
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
            ticketManagement.ManagedByApplicationUserID = ticketManagementCreateDTO.ManagedByApplicationUserID;

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

            return Ok("Ticket Management updated successfully.");
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

            return Ok("Ticket Management deleted successfully.");
        }

        private bool TicketManagementExists(int id)
        {
            return _context.TicketManagements.Any(e => e.TicketManagementID == id);
        }
    }
}
