﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMohiDatabase.Models.DTOs;
using ProjectMohiDatabase.Models;
using ProjectMohiDatabase.Models.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ProjectMohiDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketStatusesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public TicketStatusesController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        // GET: api/TicketStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketStatusDTOs>>> GetTicketStatuses()
        {
            return await _context.TicketStatuses
                .Select(ts => new TicketStatusDTOs
                {
                    StatusID = ts.StatusID,
                    StatusName = ts.StatusName
                }).ToListAsync();
        }

        // GET: api/TicketStatuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketStatusDTOs>> GetTicketStatus(int id)
        {
            // Fetch the TicketStatus data from the database
            var ticketStatus = await _context.TicketStatuses
                .FirstOrDefaultAsync(ts => ts.StatusID == id);

            if (ticketStatus == null)
            {
                return NotFound($"Ticket Status with ID {id} not found.");
            }

            // Map the entity to the DTO
            var ticketStatusDTO = new TicketStatusDTOs
            {
                StatusID = ticketStatus.StatusID,
                StatusName = ticketStatus.StatusName
            };

            return Ok(ticketStatusDTO);
        }


        // POST: api/TicketStatuses
        [HttpPost]
        public async Task<ActionResult<TicketStatus>> PostTicketStatus(TicketStatusCreateDTO ticketStatusCreateDTO)
        {
            var ticketStatus = new TicketStatus
            {
                StatusName = ticketStatusCreateDTO.StatusName
            };

            _context.TicketStatuses.Add(ticketStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketStatus", new { id = ticketStatus.StatusID }, ticketStatus);
        }

        // PUT: api/TicketStatuses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketStatus(int id, TicketStatusCreateDTO ticketStatusCreateDTO)
        {
            var ticketStatus = await _context.TicketStatuses.FindAsync(id);

            if (ticketStatus == null)
            {
                return NotFound();
            }

            ticketStatus.StatusName = ticketStatusCreateDTO.StatusName;

            _context.Entry(ticketStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(" TicketStatus Update successfully.");
        }

        // DELETE: api/TicketStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketStatus(int id)
        {
            var ticketStatus = await _context.TicketStatuses.FindAsync(id);
            if (ticketStatus == null)
            {
                return NotFound();
            }

            _context.TicketStatuses.Remove(ticketStatus);
            await _context.SaveChangesAsync();

            return Ok(" TicketStatus Delete successfully.");
        }

        private bool TicketStatusExists(int id)
        {
            return _context.TicketStatuses.Any(e => e.StatusID == id);
        }
    }
}
