using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using DAL.Context;
using DAL.Models;

namespace DBHelper.Pages
{
    public class AccountsModel : PageModel
    {
        private readonly ApplicationContext context;
        public IEnumerable<Account>? accounts;

        public AccountsModel(ApplicationContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            accounts = GetCurrentAccountsList();
        }

        public ActionResult OnPost(string name, int contactId)
        {
            try
            {
                context.Accounts?.Add(new Account { Name = name, ContactId = contactId });
                context.SaveChanges();
            }
            catch (DbUpdateException) 
            {
                return RedirectToPage("Error");
            }
            finally
            {
                accounts = GetCurrentAccountsList();
            }

            return RedirectToPage("Accounts");
        }

        private List<Account>? GetCurrentAccountsList()
        {
            return context.Accounts?.ToList();
        }
    }
}
