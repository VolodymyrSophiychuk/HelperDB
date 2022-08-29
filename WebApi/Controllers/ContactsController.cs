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
            string pattern = @"^\w+([\.-]?\w+)*";

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Contact? @new = _context.Contacts?.Where(el => el.Email == contact.Email).SingleOrDefault();

            if (@new?.Email == contact.Email)
            {
                @new.Firstname = contact.Firstname;
                @new.Lastname = contact.Lastname;
                @new.Email = contact.Email;

                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Contacts?.Add(new Contact { Firstname = contact.Firstname, Lastname = contact.Lastname, Email = contact.Email });

                await _context.SaveChangesAsync();

                _context.Accounts?.Add(new Account
                {
                    Name = Regex.Match(contact.Email, pattern).Value,
                    ContactId = _context.Contacts.OrderBy(el => el.ContactId).Last().ContactId
                });

                await _context.SaveChangesAsync();

                _context.Incidents?.Add(new Incident
                {
                    Name = string.Concat(Regex.Match(contact.Email, pattern).Value, " connection"),
                    Description = string.Concat(Regex.Match(contact.Email, pattern).Value, " was successfully connected"),
                    AccountId = _context.Accounts.OrderBy(el => el.AccountId).Last().AccountId
                });

                await _context.SaveChangesAsync();
            }

            return Ok(contact);
        }
    }
}