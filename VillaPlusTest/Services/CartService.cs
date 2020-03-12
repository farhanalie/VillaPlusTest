using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VillaPlusTest.Domain.Models;
using VillaPlusTest.Domain.Models.Exceptions;
using VillaPlusTest.Domain.Services;
using VillaPlusTest.Extensions;
using VillaPlusTest.Infrastructure;
using VillaPlusTest.Infrastructure.Entities;
using VillaPlusTest.Resources;

namespace VillaPlusTest.Services
{
    public class CartService : ICartService
    {
        private readonly VpDbContext _dbContext;
        private readonly IMapper _mapper;

        public CartService(VpDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CartResource> AddOrUpdate(AddToCartDto dto)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x=>x.Id == dto.ProductId);
            if (product == null)
                throw new BadRequestException($"Invalid {nameof(dto.ProductId)} provided");

            var add = false;
            var cart = new Cart
            {
                Items = new List<Item>()
            };
            if (dto.CartId == null)
            {
                // new cart
                cart.Items.Add(GetNewItem(dto, product));
                add = true;
            }
            else
            {
                // existing cart
                cart = await _dbContext.Carts.Include(x=>x.Items).Include(x => x.DiscountCode).FirstOrDefaultAsync(x => x.Id == dto.CartId);
                if (cart == null)
                    throw new BadRequestException($"Invalid {nameof(dto.CartId)} provided");

                var item = cart.Items.FirstOrDefault(x => x.ProductId == dto.ProductId);
                if (item == null)
                {
                    // new item in cart
                    cart.Items.Add(GetNewItem(dto, product));
                }
                else
                {
                    item.Qty = dto.Qty.GetValueOrDefault(1);
                    CalculateItemTotalAndDiscount(product, item);
                }
            }

            cart.Total = cart.Items.Sum(x => x.ItemTotal);
            cart.TotalAfterDiscount = cart.Items.Sum(x => x.ItemTotalAfterDiscount);
            if (add)
            {
                await _dbContext.AddAsync(cart);
            }
            else if(cart.DiscountCodeId != null)
            {
                cart.TotalAfterDiscount = cart.TotalAfterDiscount.GetPercent(cart.DiscountCode.DiscountPercentage);
            }
            await _dbContext.SaveChangesAsync();
            var resource = _mapper.Map<CartResource>(cart);
            return resource;
        }

        public async Task<CartResource> ApplyDiscountCode(AddDiscountCodeDto dto)
        {
            var code = await _dbContext.DiscountCodes.AsNoTracking().FirstOrDefaultAsync(x =>
                x.Code.Equals(dto.DiscountCode, StringComparison.InvariantCultureIgnoreCase));
            if (code == null)
                throw new BadRequestException($"Invalid {nameof(dto.DiscountCode)} provided");

            if (code.Expiry < DateTime.UtcNow)
                throw new BadRequestException($"Invalid {nameof(dto.DiscountCode)} provided");

            var cart = await _dbContext.Carts.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == dto.CartId);
            if (cart == null)
                throw new BadRequestException($"Invalid {nameof(dto.CartId)} provided");

            if (dto.DiscountCode.Equals(cart.DiscountCode?.Code, StringComparison.InvariantCultureIgnoreCase))
                throw new BadRequestException("Discount Code already applied");

            cart.TotalAfterDiscount = cart.Items.Sum(x => x.ItemTotalAfterDiscount).GetPercent(code.DiscountPercentage);
            cart.DiscountCode = code;
            await _dbContext.SaveChangesAsync();
            var resource = _mapper.Map<CartResource>(cart);
            return resource;
        }

        private static Item GetNewItem(AddToCartDto dto, Product product)
        {
            var item = new Item
            {
                Product = product,
                Qty = dto.Qty.GetValueOrDefault(1)
            };

            CalculateItemTotalAndDiscount(product, item);

            return item;
        }

        private static void CalculateItemTotalAndDiscount(Product product, Item item)
        {
            item.ItemTotal = item.Qty * product.Price;

            if (!product.Buy2Get1Free)
            {
                item.ItemTotalAfterDiscount = item.ItemTotal;
            }
            else
            {
                var freeItems = item.Qty / 3;
                item.ItemTotalAfterDiscount = (item.Qty - freeItems) * product.Price;
            }
        }
    }
}
