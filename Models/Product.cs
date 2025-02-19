using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zeiss.ProductApi.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // ID is generated elsewhere
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
