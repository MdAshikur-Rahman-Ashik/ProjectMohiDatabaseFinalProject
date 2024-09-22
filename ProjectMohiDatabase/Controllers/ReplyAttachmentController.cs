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
    public class ReplyAttachmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ReplyAttachmentController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // POST: api/ReplyAttachments
        [HttpPost]
        public async Task<ActionResult<ReplyAttachmentDTOs>> PostReplyAttachment([FromForm] ReplyAttachmentCreateDTOs replyAttachmentCreateDTO)
        {
            // Check if the file is uploaded
            if (replyAttachmentCreateDTO.AttachFile == null || replyAttachmentCreateDTO.AttachFile.Length == 0)
            {
                return BadRequest("File not provided.");
            }

            // Save the file to the server
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "reply_attachments");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{replyAttachmentCreateDTO.AttachFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await replyAttachmentCreateDTO.AttachFile.CopyToAsync(stream);
            }

            // Save the file info to the database
            var replyAttachment = new ReplyAttachment
            {
                ReplyID = replyAttachmentCreateDTO.ReplyID,
                AttachFile = fileName  // Storing the file name
            };

            _context.ReplyAttachments.Add(replyAttachment);
            await _context.SaveChangesAsync();

            // Return DTO with the file URL
            var replyAttachmentDTO = new ReplyAttachmentDTOs
            {
                ReplyAttachID = replyAttachment.ReplyAttachID,
                ReplyID = replyAttachment.ReplyID,
                AttachFileUrl = $"/uploads/reply_attachments/{fileName}"  // URL for accessing the file
            };

            return CreatedAtAction(nameof(GetReplyAttachment), new { id = replyAttachmentDTO.ReplyAttachID }, replyAttachmentDTO);
        }

        // GET: api/ReplyAttachments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReplyAttachmentDTOs>> GetReplyAttachment(int id)
        {
            var replyAttachment = await _context.ReplyAttachments.FindAsync(id);

            if (replyAttachment == null)
            {
                return NotFound();
            }

            var replyAttachmentDTO = new ReplyAttachmentDTOs
            {
                ReplyAttachID = replyAttachment.ReplyAttachID,
                ReplyID = replyAttachment.ReplyID,
                AttachFileUrl = $"/uploads/reply_attachments/{replyAttachment.AttachFile}"
            };

            return replyAttachmentDTO;
        }

        // DELETE: api/ReplyAttachments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReplyAttachment(int id)
        {
            var replyAttachment = await _context.ReplyAttachments.FindAsync(id);
            if (replyAttachment == null)
            {
                return NotFound();
            }

            // Delete the file from the server
            var filePath = Path.Combine(_environment.WebRootPath, "uploads", "reply_attachments", replyAttachment.AttachFile);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // Remove the record from the database
            _context.ReplyAttachments.Remove(replyAttachment);
            await _context.SaveChangesAsync();

            return Ok("ReplyAttachment Delete successfully.");
        }
    }
}
