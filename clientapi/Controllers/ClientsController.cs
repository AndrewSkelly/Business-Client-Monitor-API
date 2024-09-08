using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clientapi.Data;
using clientapi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clientapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.client.ToListAsync();
        }

        // POST: api/Clients
        [HttpPost]
        public async Task<ActionResult<Client>> PostClients(Client client)
        {
            _context.client.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClientById), new { id = client.clientid }, client);
        }

        // GET: api/Clients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var client = await _context.client.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // DELETE: api/Clients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            // Find the client by ID
            var client = await _context.client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            // Remove the client from the context
            _context.client.Remove(client);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a NoContent response to indicate successful deletion
            return NoContent();
        }
    }
}
