using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMohiDatabase.Models.DAL;
using ProjectMohiDatabase.Models;
using ProjectMohiDatabase.Models.DTOs;

namespace ProjectMohiDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritieController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrioritieController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/priorities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriorityDTOs>>> GetPriorities()
        {
            var priorities = await _context.Priorities
                .Select(p => new PriorityDTOs
                {
                    PriorityID = p.PriorityID,
                    PriorityName = p.PriorityName
                }).ToListAsync();

            return Ok(priorities);
        }

        // GET: api/priorities/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PriorityDTOs>> GetPriority(int id)
        {
            // Fetch the result directly from the stored procedure
            var priority = await _context.Priorities
                .FromSqlRaw("EXEC GetPriorityByID @PriorityID = {0}", id)
                .ToListAsync(); // Get the result as a list

            // Manually map the result to your DTO
            var priorityDTO = priority.Select(p => new PriorityDTOs
            {
                PriorityID = p.PriorityID,
                PriorityName = p.PriorityName
            }).FirstOrDefault();

            if (priorityDTO == null)
            {
                return NotFound($"Priority with ID {id} not found.");
            }

            return Ok(priorityDTO);
        }


        // POST: api/priorities
        [HttpPost]
        public async Task<ActionResult<PriorityDTOs>> PostPriority([FromForm] PriorityDTOs priorityDto)
        {
            var priority = new Priority
            {
                PriorityName = priorityDto.PriorityName
            };

            _context.Priorities.Add(priority);
            await _context.SaveChangesAsync();

            priorityDto.PriorityID = priority.PriorityID; // Set the ID from the new record

            return CreatedAtAction(nameof(GetPriority), new { id = priority.PriorityID }, priorityDto);
        }

        // PUT: api/priorities/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriority(int id, [FromForm] PriorityDTOs priorityDto)
        {
            // Check if the ID in the route matches the ID in the DTO
            if (id != priorityDto.PriorityID)
            {
                return BadRequest("Priority ID mismatch.");
            }

            // Find the existing priority in the database
            var priority = await _context.Priorities.FindAsync(id);
            if (priority == null)
            {
                return NotFound($"Priority with ID {id} not found.");
            }

            // Update the priority properties
            priority.PriorityName = priorityDto.PriorityName;

            // Mark the entity as modified
            _context.Entry(priority).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriorityExists(id))
                {
                    return NotFound($"Priority with ID {id} no longer exists.");
                }
                else
                {
                    throw; // Re-throw the exception if it's not a concurrency issue
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok("Priority updated successfully.");
        }


        // DELETE: api/priorities/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriority(int id)
        {
            var priority = await _context.Priorities.FindAsync(id);
            if (priority == null)
            {
                return NotFound($"Priority with ID {id} not found.");
            }

            _context.Priorities.Remove(priority);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a priority exists
        private bool PriorityExists(int id)
        {
            return _context.Priorities.Any(e => e.PriorityID == id);
        }
    }
}
