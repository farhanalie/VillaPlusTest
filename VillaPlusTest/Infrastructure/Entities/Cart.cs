using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaPlusTest.Infrastructure.Entities
{
    public class Cart : BaseEntity
    {
        public decimal? Total { get; set; }
        public decimal? TotalAfterDiscount { get; set; }

        public List<Item> Items { get; set; }

        public int? DiscountCodeId { get; set; }
        [ForeignKey(nameof(DiscountCodeId))]
        public DiscountCode DiscountCode { get; set; }
    }
}