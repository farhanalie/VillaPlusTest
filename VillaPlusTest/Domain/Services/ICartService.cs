using System.Threading.Tasks;
using VillaPlusTest.Domain.Models;
using VillaPlusTest.Resources;

namespace VillaPlusTest.Domain.Services
{
    public interface ICartService
    {
        Task<CartResource> AddOrUpdate(AddToCartDto dto);
        Task<CartResource> ApplyDiscountCode(AddDiscountCodeDto dto);
    }
}