namespace VillaPlusTest.Domain.Models
{
    public class AddToCartDto
    {
        public int? CartId { get; set; }
        public int ProductId { get; set; }
        public int? Qty { get; set; }
    }
}