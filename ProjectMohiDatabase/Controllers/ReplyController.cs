using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMohiDatabase.Models.DAL;
using ProjectMohiDatabase.Models;
using ProjectMohiDatabase.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ProjectMohiDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ReplyController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        // GET: api/replies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReplyDTOs>>> GetReplies()
        {
            var replies = await _context.Replies
                .Include(r => r.TicketSupport) // Include related entities if necessary
            .Include(r => r.ApplicationUser)
                .Select(r => new ReplyDTOs
                {
                    ReplyID = r.ReplyID,
                    TicketSupportID = r.TicketSupportID,
                    ApplicationUserID = r.ApplicationUserID,
                    Description = r.Description,
                    UpdatedAt = r.UpdatedAt,
                    ReplyAttachmentIds = r.ReplyAttachments.Select(a => a.ReplyAttachID).ToList() // Assuming there's an Id property in ReplyAttachment
                })
                .ToListAsync();

            return Ok(replies);
        }

        // GET: api/replies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReplyDTOs>> GetReply(int id)
        {
            var reply = await _context.Replies
                .Include(r => r.ReplyAttachments) // Include ReplyAttachments
                .FirstOrDefaultAsync(r => r.ReplyID == id);

            if (reply == null)
            {
                return NotFound($"Reply with ID {id} not found.");
            }

            // Map the Reply to ReplyDTOs
            var replyDTO = new ReplyDTOs
            {
                ReplyID = reply.ReplyID,
                TicketSupportID = reply.TicketSupportID,
                ApplicationUserID = reply.ApplicationUserID,
                Description = reply.Description,
                UpdatedAt = reply.UpdatedAt,
                ReplyAttachmentIds = reply.ReplyAttachments.Select(ra => ra.ReplyAttachID).ToList() // Map attachment IDs
            };

            return Ok(replyDTO);
        }




        // POST: api/replies
        [HttpPost]
        public async Task<ActionResult<ReplyDTOs>> PostReply(ReplyDTOs replyDto)
        {
            var reply = new Reply
            {
                TicketSupportID = replyDto.TicketSupportID,
                ApplicationUserID = replyDto.ApplicationUserID,
                Description = replyDto.Description,
                UpdatedAt = DateTime.UtcNow // Set current time
            };

            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();

            replyDto.ReplyID = reply.ReplyID; // Set the ID from the new record

            return CreatedAtAction(nameof(GetReply), new { id = reply.ReplyID }, replyDto);
        }
        

        // PUT: api/replies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReply(int id, ReplyDTOs replyDto)
        {
            if (id != replyDto.ReplyID)
            {
                return BadRequest("Reply ID mismatch.");
            }

            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                return NotFound($"Reply with ID {id} not found.");
            }

            reply.Description = replyDto.Description;
            reply.UpdatedAt = DateTime.UtcNow; // Update timestamp

            _context.Entry(reply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyExists(id))
                {
                    return NotFound($"Reply with ID {id} no longer exists.");
                }
                else
                {
                    throw; // Re-throw if it's another issue
                }
            }

            return Ok("Reply updated successfully.");
        }

        // DELETE: api/replies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReply(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                return NotFound($"Reply with ID {id} not found.");
            }

            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();

            return Ok("Reply Delete successfully.");
        }

        // Helper method to check if a reply exists
        private bool ReplyExists(int id)
        {
            return _context.Replies.Any(e => e.ReplyID == id);
        }
    }
}
