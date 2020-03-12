using System.ComponentModel.DataAnnotations;

namespace VillaPlusTest.Infrastructure.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        public bool Buy2Get1Free { get; set; }
        public bool Active { get; set; }
    }
}
