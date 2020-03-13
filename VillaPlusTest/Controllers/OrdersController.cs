using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VillaPlus.API.Domain.Models;
using VillaPlus.API.Domain.Services;
using VillaPlus.API.Resources;

namespace VillaPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly ICartService _cartService;

        public OrdersController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("AddToCart")]
        public async Task<CartResource> AddToCart(AddToCartDto dto)
        {
            return await _cartService.AddOrUpdate(dto);
        }

        [HttpPut("ApplyDiscountCode")]
        public async Task<CartResource> AddToCart(AddDiscountCodeDto dto)
        {
            return await _cartService.ApplyDiscountCode(dto);
        }
        //[HttpGet]
        //public async Task<CartResource> Get()
        //{
        //    var dto1 = new AddToCartDto
        //    {
        //        ProductId = 2,
        //    };

        //    var r1 = await _cartService.AddOrUpdate(dto1);

        //    var dto2 = new AddToCartDto
        //    {
        //        ProductId = 2,
        //        CartId = r1.Id,
        //        Qty = 4
        //    };

        //    var r2 = await _cartService.AddOrUpdate(dto2);

        //    var dto4 = new AddToCartDto
        //    {
        //        ProductId = 5,
        //        CartId = r1.Id,
        //        Qty = 4
        //    };

        //    var r4 = await _cartService.AddOrUpdate(dto4);

        //    var d1 = new AddDiscountCodeDto
        //    {
        //        DiscountCode = "NEWCUSTOMER",
        //        CartId = r2.Id
        //    };

        //    var rd = await _cartService.ApplyDiscountCode(d1);

        //    var dto3 = new AddToCartDto
        //    {
        //        ProductId = 2,
        //        CartId = rd.Id,
        //        Qty = 8
        //    };

        //    var r3 = await _cartService.AddOrUpdate(dto3);
        //    return r3;
        //}

    }
}