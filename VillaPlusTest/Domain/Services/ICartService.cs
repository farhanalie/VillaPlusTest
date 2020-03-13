using System.Threading.Tasks;
using VillaPlus.API.Domain.Models;
using VillaPlus.API.Resources;

namespace VillaPlus.API.Domain.Services
{
    public interface ICartService
    {
        Task<CartResource> AddOrUpdate(AddToCartDto dto);
        Task<CartResource> ApplyDiscountCode(AddDiscountCodeDto dto);
    }
}