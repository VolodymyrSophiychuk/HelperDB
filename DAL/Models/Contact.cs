using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Contact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
        [MinLength(1), MaxLength(32), Required]
        public string? Firstname { get; set; }
        [MinLength(1), MaxLength(32), Required]
        public string? Lastname { get; set; }
        [Required]
        public string? Email { get; set; }

        public List<Account>? Accounts { get; set; }
    }
}
