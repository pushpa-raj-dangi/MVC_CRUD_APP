using System.ComponentModel.DataAnnotations;

namespace ProductCRUD.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public required string Name { get; set; }

        public required string Description { get; set; }
        public string? Image { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
