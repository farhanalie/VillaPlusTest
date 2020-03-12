namespace VillaPlusTest.Resources
{
    public class ItemResource : BaseResource
    {
        public int Qty { get; set; }
        public decimal? ItemTotal { get; set; }
        public decimal? ItemTotalAfterDiscount { get; set; }
        public ProductResource Product { get; set; }
    }
}