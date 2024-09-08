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

        // GET: api/Clients
        [HttpPost]
        public async Task<ActionResult<Client>> PostClients(Client client)
        {
            // Add the new client to the database context
            _context.client.Add(client);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 201 Created response with the newly created client data
            return CreatedAtAction(nameof(GetClientById), new { id = client.clientid }, client);
        }

        // Helper method for retrieving a specific client by ID (used in CreatedAtAction)
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
    }
}
