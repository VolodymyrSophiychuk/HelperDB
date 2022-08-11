using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using DAL.Context;
using DAL.Models;

namespace DBHelper.Pages
{
    public class IncidentsModel : PageModel
    {
        private readonly ApplicationContext context;
        public IEnumerable<Incident>? incidents;

        public IncidentsModel(ApplicationContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            incidents = GetCurrentIncidentsList();
        }

        public ActionResult OnPost(string name, string description, int accountId)
        {
            try
            {
                context.Incidents?.Add(new Incident { Name = name, Description = description, AccountId = accountId });
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return RedirectToPage("Error");
            }
            finally
            {
                incidents = GetCurrentIncidentsList();
            }

            return RedirectToPage("Incidents");
        }

        private List<Incident>? GetCurrentIncidentsList() => context.Incidents?.ToList();
    }
}
