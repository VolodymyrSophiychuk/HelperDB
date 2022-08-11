using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Incident
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IncidentId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }

        public int AccountId { get; set; }
        public virtual Account? Account { get; set; }
    }
}
