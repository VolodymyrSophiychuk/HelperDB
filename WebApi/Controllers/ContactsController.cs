using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using DAL.Context;
using DAL.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private ApplicationContext _context;

        public ContactsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get() => await _context.Contacts?.ToListAsync();
        [HttpPost]
        public async Task<ActionResult<Contact>> Post(Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Contact? @new = _context.Contacts?.Where(el => el.Email == contact.Email).Select(id => id).Single();

                @new.Firstname = contact.Firstname;
                @new.Lastname = contact.Lastname;
                @new.Email = contact.Email;

                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException)
            {
                try
                {
                    _context.Contacts?.Add(new Contact { Firstname = contact.Firstname, Lastname = contact.Lastname, Email = contact.Email });

                    await _context.SaveChangesAsync();

                    _context.Accounts?.Add(new Account
                    {
                        Name = Regex.Match(contact.Email, @"[A-Za-z0-9._%+-]+").Value,
                        ContactId = _context.Contacts.OrderBy(el => el.ContactId).Last().ContactId
                    });

                    await _context.SaveChangesAsync();

                    _context.Incidents?.Add(new Incident
                    {
                        Name = string.Concat(Regex.Match(contact.Email, @"[A-Za-z0-9._%+-]+").Value, " connection"),
                        Description = string.Concat(Regex.Match(contact.Email, @"[A-Za-z0-9._%+-]+").Value, " was successfully connected"),
                        AccountId = _context.Accounts.OrderBy(el => el.AccountId).Last().AccountId
                    });

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException) { }
            }

            return Ok(contact);
        }
    }
}