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
    public class TicketSupportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketSupportController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TicketSupports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketSupportDTOs>>> GetTicketSupports()
        {
            return await _context.TicketSupports
                .Select(ts => new TicketSupportDTOs
                {
                    TicketSupportID = ts.TicketSupportID,
                    PersonID = ts.PersonID,
                    PackageID = ts.PackageID,
                    StatusID = ts.StatusID,
                    Email = ts.Email,
                    PriorityID = ts.PriorityID,
                    DepartmentID = ts.DepartmentID,
                    Subject = ts.Subject,
                    Description = ts.Description,
                    TicketAttachments = ts.TicketAttachments.Select(ta => new TicketAttachmentDTOs
                    {
                        TicketAttachID = ta.TicketAttachID,
                        TicketSupportID = ta.TicketSupportID,
                        AttachFileUrl = ta.AttachFile
                    }).ToList(),
                    TicketSupportStatusHistories = ts.TicketSupportStatusHistories.Select(tsh => new TicketSupportStatusHistoryDTOs
                    {
                        // Map the fields of TicketSupportStatusHistory
                    }).ToList(),
                    TicketManagements = ts.TicketManagements.Select(tm => new TicketManagementDTOs
                    {
                        // Map the fields of TicketManagement
                    }).ToList(),
                    Replies = ts.Replies.Select(r => new ReplyDTOs
                    {
                        // Map the fields of Reply
                    }).ToList()
                }).ToListAsync();
        }

        // GET: api/TicketSupports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketSupportDTOs>> GetTicketSupport(int id)
        {
            var ticketSupportData = await _context.TicketSupports
                .FromSqlInterpolated($"EXEC GetTicketSupportByID @TicketSupportID = {id}")
                .Select(ts => new TicketSupportDTOs
                {
                    TicketSupportID = ts.TicketSupportID,
                    PersonID = ts.PersonID,
                    PackageID = ts.PackageID,
                    StatusID = ts.StatusID,
                    Email = ts.Email,
                    PriorityID = ts.PriorityID,
                    DepartmentID = ts.DepartmentID,
                    Subject = ts.Subject,
                    Description = ts.Description,
                    TicketAttachments = ts.TicketAttachments.Select(ta => new TicketAttachmentDTOs
                    {
                        TicketAttachID = ta.TicketAttachID,
                        TicketSupportID = ta.TicketSupportID,
                        AttachFileUrl = ta.AttachFile,
                    }).ToList(),
                    TicketSupportStatusHistories = ts.TicketSupportStatusHistories.Select(tsh => new TicketSupportStatusHistoryDTOs
                    {
                        // Map the fields of TicketSupportStatusHistory
                    }).ToList(),
                    TicketManagements = ts.TicketManagements.Select(tm => new TicketManagementDTOs
                    {
                        // Map the fields of TicketManagement
                    }).ToList(),
                    Replies = ts.Replies.Select(r => new ReplyDTOs
                    {
                        // Map the fields of Reply
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (ticketSupportData == null)
            {
                return NotFound();
            }

            return Ok(ticketSupportData);
        }


        // POST: api/TicketSupports
        [HttpPost]
        public async Task<ActionResult<TicketSupport>> PostTicketSupport( [FromForm]TicketSupportCreateDTO ticketSupportCreateDTO)
        {
            // Check if Department exists
            var departmentExists = await _context.Departments.AnyAsync(d => d.DepartmentID == ticketSupportCreateDTO.DepartmentID);
            if (!departmentExists)
            {
                return BadRequest("Invalid DepartmentID. The specified department does not exist.");
            }

            // Proceed with the insertion
            var ticketSupport = new TicketSupport
            {
                PersonID = ticketSupportCreateDTO.PersonID,
                PackageID = ticketSupportCreateDTO.PackageID,
                StatusID = ticketSupportCreateDTO.StatusID,
                Email = ticketSupportCreateDTO.Email,
                PriorityID = ticketSupportCreateDTO.PriorityID,
                DepartmentID = ticketSupportCreateDTO.DepartmentID,
                Subject = ticketSupportCreateDTO.Subject,
                Description = ticketSupportCreateDTO.Description,
            };

            _context.TicketSupports.Add(ticketSupport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketSupport", new { id = ticketSupport.TicketSupportID }, ticketSupport);
        }


        // PUT: api/TicketSupports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketSupport(int id, [FromForm] TicketSupportCreateDTO ticketSupportCreateDTO)
        {
            if (id != ticketSupportCreateDTO.PersonID)
            {
                return BadRequest();
            }

            var ticketSupport = await _context.TicketSupports.FindAsync(id);

            if (ticketSupport == null)
            {
                return NotFound();
            }

            ticketSupport.PersonID = ticketSupportCreateDTO.PersonID;
            ticketSupport.PackageID = ticketSupportCreateDTO.PackageID;
            ticketSupport.StatusID = ticketSupportCreateDTO.StatusID;
            ticketSupport.Email = ticketSupportCreateDTO.Email;
            ticketSupport.PriorityID = ticketSupportCreateDTO.PriorityID;
            ticketSupport.DepartmentID = ticketSupportCreateDTO.DepartmentID;
            ticketSupport.Subject = ticketSupportCreateDTO.Subject;
            ticketSupport.Description = ticketSupportCreateDTO.Description;

            _context.Entry(ticketSupport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketSupportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(" TicketSupport Update successfully.");
        }

        // DELETE: api/TicketSupports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketSupport(int id)
        {
            var ticketSupport = await _context.TicketSupports.FindAsync(id);
            if (ticketSupport == null)
            {
                return NotFound();
            }

            _context.TicketSupports.Remove(ticketSupport);
            await _context.SaveChangesAsync();

            return Ok(" TicketSupport Delete successfully.");
        }

        private bool TicketSupportExists(int id)
        {
            return _context.TicketSupports.Any(e => e.TicketSupportID == id);
        }
    }
}
