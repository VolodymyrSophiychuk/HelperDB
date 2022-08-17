using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Contact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
        [MinLength(1), MaxLength(32), Required(ErrorMessage = "Firstname is required")]
        public string? Firstname { get; set; }
        [MinLength(1), MaxLength(32), Required(ErrorMessage = "Lastname is required")]
        public string? Lastname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect mail address")]
        public string? Email { get; set; }

        public List<Account>? Accounts { get; set; }
    }
}
