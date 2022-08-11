using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;
using DAL.Context;
using DAL.Models;

namespace DBHelper.Pages
{
    public class ContactsModel : PageModel
    {
        private readonly ApplicationContext context;
        public IEnumerable<Contact>? contacts;

        public ContactsModel(ApplicationContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            contacts = GetCurrentContactsList();
        }

        public ActionResult OnPost(string firstname, string lastname, string email)
        {
            try
            {
                Contact? @new = context.Contacts?.Where(el => el.Email == email).Select(id => id).Single();

                @new.Firstname = firstname;
                @new.Lastname = lastname;
                @new.Email = email;

                context.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                try
                {
                    context.Contacts?.Add(new Contact { Firstname = firstname, Lastname = lastname, Email = email });

                    context.SaveChanges();

                    context.Accounts?.Add(new Account { Name = string.Concat(lastname, new Random().Next(10, 99)),
                        ContactId = context.Contacts.OrderBy(el => el.ContactId).Last().ContactId });

                    context.SaveChanges();

                    context.Incidents?.Add(new Incident { Name = string.Concat(context.Accounts.OrderBy(el => el.AccountId).Last().Name, " connection"),
                        Description = string.Concat(context.Accounts.OrderBy(el => el.AccountId).Last().Name, " was successfully connected"),
                        AccountId = context.Accounts.OrderBy(el => el.AccountId).Last().AccountId});

                    context.SaveChanges();
                }
                catch (DbUpdateException e) 
                {
                    return RedirectToPage("Error", e.InnerException.Message);
                }
            }
            finally
            {
                contacts = GetCurrentContactsList();
            }

            return RedirectToPage("Contacts");
        }

        private List<Contact>? GetCurrentContactsList()
        {
            return context.Contacts?.ToList();
        }
    }
}
