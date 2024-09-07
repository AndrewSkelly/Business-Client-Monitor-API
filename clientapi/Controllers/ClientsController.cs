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
    }
}
