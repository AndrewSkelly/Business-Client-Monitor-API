using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clientapi.Data;
using clientapi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clientapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ServiceDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDetails>>> GetServiceHistories()
        {
            return await _context.servicehistory.ToListAsync();
        }

        // GET: api/ServiceDetails/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDetails>> GetServiceHistory(int id)
        {
            var serviceHistory = await _context.servicehistory.FindAsync(id);

            if (serviceHistory == null)
            {
                return NotFound();
            }

            return serviceHistory;
        }

        // POST: api/ServiceDetails
        [HttpPost]
        public async Task<ActionResult<ServiceDetails>> PostServiceHistory(ServiceDetails serviceHistory)
        {
            _context.servicehistory.Add(serviceHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceHistory), new { id = serviceHistory.servicehistoryid }, serviceHistory);
        }

        // PUT: api/ServiceDetails/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceHistory(int id, ServiceDetails serviceHistory)
        {
            // Check if the ServiceHistory ID in the URL matches the ServiceHistory ID in the request body
            if (id != serviceHistory.servicehistoryid)
            {
                return BadRequest("ServiceHistory ID mismatch");
            }

            // Check if the ServiceHistory exists in the database
            var existingServiceHistory = await _context.servicehistory.FindAsync(id);
            if (existingServiceHistory == null)
            {
                return NotFound();
            }

            // Update the ServiceHistory's properties with the new data
            existingServiceHistory.clientid = serviceHistory.clientid;
            existingServiceHistory.staffid = serviceHistory.staffid;
            existingServiceHistory.servicetype = serviceHistory.servicetype;
            existingServiceHistory.servicedate = serviceHistory.servicedate;

            // Mark the entity as modified
            _context.Entry(existingServiceHistory).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the ServiceHistory still exists in the database
                if (!ServiceHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Return NoContent to indicate success with no content to return
            return NoContent();
        }

        // DELETE: api/ServiceDetails/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceHistory(int id)
        {
            // Find the ServiceHistory by ID
            var serviceHistory = await _context.servicehistory.FindAsync(id);
            if (serviceHistory == null)
            {
                return NotFound();
            }

            // Remove the ServiceHistory from the context
            _context.servicehistory.Remove(serviceHistory);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a NoContent response to indicate successful deletion
            return NoContent();
        }

        // Helper method to check if a ServiceHistory exists by ID
        private bool ServiceHistoryExists(int id)
        {
            return _context.servicehistory.Any(e => e.servicehistoryid == id);
        }
    }
}
