using System.Collections.Generic;

namespace VillaPlus.API.Resources
{
    public class CartResource : BaseResource
    {
        public decimal? Total { get; set; }
        public decimal? TotalAfterDiscount { get; set; }
        public List<ItemResource> Items { get; set; }
    }
}