using System.Collections.Generic;

namespace VillaPlusTest.Resources
{
    public class CartResource : BaseResource
    {
        public decimal? Total { get; set; }
        public decimal? TotalAfterDiscount { get; set; }
        public List<ItemResource> Items { get; set; }
    }
}