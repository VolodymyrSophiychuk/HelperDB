using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Index("Name", IsUnique = true)]
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Account name is required")]
        public string? Name { get; set; }

        [Range(1, 10000, ErrorMessage = "Contact identificator is required")]
        public int ContactId { get; set; }
        public virtual Contact? Contact { get; set; }

        public List<Incident>? Incidents { get; set; }
    }
}
