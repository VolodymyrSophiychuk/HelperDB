using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using DAL.Context;
using DAL.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private ApplicationContext _context;

        public AccountsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get() => await _context.Accounts?.ToListAsync();
        [HttpPost]
        public async Task<ActionResult<Account>> Post(Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_context.Contacts?.Where(el => el.ContactId == account.ContactId).Select(el => el).ToList().Count != 0)
            {
                if (_context.Accounts?.Where(el => el.Name == account.Name).Select(el => el).ToList().Count == 0)
                {
                    _context.Accounts?.Add(new Account { Name = account.Name, ContactId = account.ContactId });

                    await _context.SaveChangesAsync();
                }
            }

            return Ok(account);
        }
    }
}
