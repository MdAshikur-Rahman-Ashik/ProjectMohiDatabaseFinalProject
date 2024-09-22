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
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTOs>>> GetDepartments()
        {
            var departments = await _context.Departments
                .Select(d => new DepartmentDTOs
                {
                    DepartmentID = d.DepartmentID,
                    Name = d.Name,
                    Description = d.Description
                }).ToListAsync();

            return Ok(departments);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
       
        public async Task<ActionResult<DepartmentDTOs>> GetDepartment(int id)
        {
            // Fetch the result directly from the stored procedure
            var department = await _context.Departments
                .FromSqlRaw("EXEC GetDepartmentByID @DepartmentID = {0}", id)
                .ToListAsync(); // Get the result as a list

            // Manually map the result to your DTO
            var departmentDTO = department.Select(d => new DepartmentDTOs
            {
                DepartmentID = d.DepartmentID,
                Name = d.Name,
                Description = d.Description
            }).FirstOrDefault();

            if (departmentDTO == null)
            {
                return NotFound();
            }

            return Ok(departmentDTO);
        }





        // POST: api/departments
        [HttpPost]
        public async Task<ActionResult<DepartmentDTOs>> PostDepartment([FromForm] DepartmentDTOs departmentDto)
        {
            var department = new Department
            {
                Name = departmentDto.Name,
                Description = departmentDto.Description
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            departmentDto.DepartmentID = department.DepartmentID; // Set the ID from the new record

            return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentID }, departmentDto);
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, [FromForm] DepartmentDTOs departmentDto)
        {
            if (id != departmentDto.DepartmentID)
            {
                return BadRequest("Department ID mismatch.");
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            // Update the properties
            department.Name = departmentDto.Name;
            department.Description = departmentDto.Description;

            // Mark the entity as modified
            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound($"Department with ID {id} no longer exists.");
                }
                else
                {
                    throw; // Re-throw the exception if it's not a concurrency issue
                }
            }

            return Ok("Department updated successfully.");
        }

        // Helper method to check if a department exists
        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentID == id);
        }


        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return Ok("Delete Department Successfully");
        }
    }
}
