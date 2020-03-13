namespace VillaPlus.API.Domain.Models
{
    public class AddDiscountCodeDto
    {
        public int CartId { get; set; }
        public string DiscountCode { get; set; }
    }
}