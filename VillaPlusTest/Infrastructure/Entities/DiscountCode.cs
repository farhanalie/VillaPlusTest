using System;
using System.ComponentModel.DataAnnotations;

namespace VillaPlus.API.Infrastructure.Entities
{
    public class DiscountCode : BaseEntity
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public int DiscountPercentage { get; set; }
        [Required]
        public DateTime Expiry { get; set; }
    }
}
