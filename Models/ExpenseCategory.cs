using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_Horizon.Models
{
    public class ExpenseCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [StringLength(250)]
        public string? Description { get; set; }

        // Optional: this lets you attach a color/icon for UI purposes
        public string? Color { get; set; }
    }
}
