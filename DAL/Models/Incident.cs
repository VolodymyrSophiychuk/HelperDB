using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Incident
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IncidentId { get; set; }
        [Required(ErrorMessage = "Incident name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Incident description is required")]
        public string? Description { get; set; }

        [Range(1, 10000, ErrorMessage = "Contact identificator is required")]
        public int AccountId { get; set; }
        public virtual Account? Account { get; set; }
    }
}
