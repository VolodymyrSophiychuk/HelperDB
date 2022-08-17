using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using DAL.Context;
using DAL.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentsController : ControllerBase
    {
        private ApplicationContext _context;

        public IncidentsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> Get() => await _context.Incidents?.ToListAsync();
        [HttpPost]
        public async Task<ActionResult<Incident>> Post(Incident incident)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_context.Accounts?.Where(el => el.AccountId == incident.AccountId).Select(el => el).ToList().Count != 0)
            {
                _context.Incidents?.Add(new Incident { Name = incident.Name, Description = incident.Description, AccountId = incident.AccountId });

                await _context.SaveChangesAsync();
            }

            return Ok(incident);
        }
    }
}
