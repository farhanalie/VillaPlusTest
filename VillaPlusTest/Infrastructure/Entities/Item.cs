using System.ComponentModel.DataAnnotations.Schema;

namespace VillaPlusTest.Infrastructure.Entities
{
    public class Item : BaseEntity
    {
        public int Qty { get; set; }
        public decimal? ItemTotal { get; set; }
        public decimal? ItemTotalAfterDiscount { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}