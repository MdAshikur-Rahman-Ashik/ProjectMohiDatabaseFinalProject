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
        private readonly IWebHostEnvironment _hostEnvironment;

        public TicketAttachmentController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // POST: api/TicketAttachment
        [HttpPost]
        public async Task<IActionResult> PostTicketAttachment([FromForm] TicketAttachmentCreateDTOs ticketAttachmentCreateDTO)
        {
            if (ticketAttachmentCreateDTO.AttachFile == null || ticketAttachmentCreateDTO.AttachFile.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Validate TicketSupportID
            var ticketSupport = await _context.TicketSupports.FindAsync(ticketAttachmentCreateDTO.TicketSupportID);
            if (ticketSupport == null)
            {
                return NotFound("TicketSupport not found.");
            }

            // Save the uploaded file
            var filePath = await SaveFile(ticketAttachmentCreateDTO.AttachFile);

            // Create TicketAttachment entity
            var ticketAttachment = new TicketAttachment
            {
                TicketSupportID = ticketAttachmentCreateDTO.TicketSupportID,
                AttachFile = filePath
            };

            _context.TicketAttachments.Add(ticketAttachment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicketAttachment), new { id = ticketAttachment.TicketAttachID }, ticketAttachment);
        }

        // GET: api/TicketAttachment/{id}
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
                AttachFile = ticketAttachment.AttachFile
            };

            return ticketAttachmentDTO;
        }

        // Method to save the uploaded file to a directory
        private async Task<string> SaveFile(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Returning the file path relative to the uploads folder
            return Path.Combine("images", fileName);
        }
    }
}
