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
        [Required]
        public string? Name { get; set; }

        public int ContactId { get; set; }
        public virtual Contact? Contact { get; set; }

        public List<Incident>? Incidents { get; set; }
    }
}
