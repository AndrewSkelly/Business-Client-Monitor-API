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

        // PUT: api/Clients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, Client updatedClient)
        {
            // Check if the client ID in the URL matches the client ID in the request body
            if (id != updatedClient.clientid)
            {
                return BadRequest("Client ID mismatch");
            }

            // Check if the client exists in the database
            var client = await _context.client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            // Update the client's properties with the new data
            client.name = updatedClient.name;
            client.email = updatedClient.email;
            client.phone = updatedClient.phone;
            client.tags = updatedClient.tags;
            client.notes = updatedClient.notes;

            // Mark the entity as modified
            _context.Entry(client).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the client still exists in the database
                if (!ClientExists(id))
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

        // Helper method to check if a client exists by ID
        private bool ClientExists(int id)
        {
            return _context.client.Any(e => e.clientid == id);
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
