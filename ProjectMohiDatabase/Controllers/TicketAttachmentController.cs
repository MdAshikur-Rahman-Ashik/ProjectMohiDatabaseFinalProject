using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMohiDatabase.Models.DAL;
using ProjectMohiDatabase.Models;
using ProjectMohiDatabase.Models.DTOs;

namespace ProjectMohiDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketAttachmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TicketAttachmentController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // POST: api/TicketAttachments
        [HttpPost]
        public async Task<ActionResult<TicketAttachmentDTOs>> PostTicketAttachment([FromForm] TicketAttachmentCreateDTOs ticketAttachmentCreateDTO)
        {
            // Check if the file is uploaded
            if (ticketAttachmentCreateDTO.AttachFile == null || ticketAttachmentCreateDTO.AttachFile.Length == 0)
            {
                return BadRequest("File not provided.");
            }

            // Validate TicketSupportID
            var ticketSupport = await _context.TicketSupports.FindAsync(ticketAttachmentCreateDTO.TicketSupportID);
            if (ticketSupport == null)
            {
                return NotFound("TicketSupport not found.");
            }

            // Save the file to the server
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "ticket_attachments");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{ticketAttachmentCreateDTO.AttachFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ticketAttachmentCreateDTO.AttachFile.CopyToAsync(stream);
            }

            // Save the file info to the database
            var ticketAttachment = new TicketAttachment
            {
                TicketSupportID = ticketAttachmentCreateDTO.TicketSupportID,
                AttachFile = fileName  // Storing the file name
            };

            _context.TicketAttachments.Add(ticketAttachment);
            await _context.SaveChangesAsync();

            // Return DTO with the file URL
            var ticketAttachmentDTO = new TicketAttachmentDTOs
            {
                TicketAttachID = ticketAttachment.TicketAttachID,
                TicketSupportID = ticketAttachment.TicketSupportID,
                AttachFileUrl = $"/uploads/ticket_attachments/{fileName}"  // URL for accessing the file
            };

            return CreatedAtAction(nameof(GetTicketAttachment), new { id = ticketAttachmentDTO.TicketAttachID }, ticketAttachmentDTO);
            
        }

        // GET: api/TicketAttachments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketAttachmentDTOs>> GetTicketAttachment(int id)
        {
            var ticketAttachment = await _context.TicketAttachments.FindAsync(id);

            if (ticketAttachment == null)
            {
                return NotFound();
            }

            var ticketAttachmentDTO = new TicketAttachmentDTOs
            {
                TicketAttachID = ticketAttachment.TicketAttachID,
                TicketSupportID = ticketAttachment.TicketSupportID,
                AttachFileUrl = $"/uploads/ticket_attachments/{ticketAttachment.AttachFile}"
            };

            return ticketAttachmentDTO;
        }

        // DELETE: api/TicketAttachments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketAttachment(int id)
        {
            var ticketAttachment = await _context.TicketAttachments.FindAsync(id);
            if (ticketAttachment == null)
            {
                return NotFound();
            }

            // Delete the file from the server
            var filePath = Path.Combine(_environment.WebRootPath, "uploads", "ticket_attachments", ticketAttachment.AttachFile);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // Remove the record from the database
            _context.TicketAttachments.Remove(ticketAttachment);
            await _context.SaveChangesAsync();

            return Ok(" TicketAttachment Delete successfully.");
        }
    }
}

