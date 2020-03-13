using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using VillaPlus.API.Domain.Models;
using VillaPlus.API.Infrastructure;
using VillaPlus.API.Mappings;
using VillaPlus.API.Services;
using Xunit;

namespace VillaPlus.Tests
{
    public class CartServiceUnitTest
    {
        private VpDbContext GetInMemoryDbContext()
        {
            var builder = new DbContextOptionsBuilder<VpDbContext>();
            builder.UseInMemoryDatabase("VillaTest");
            var options = builder.Options;
            var db = new VpDbContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            DbInitializer.AddTestData(db);
            return db;
        }

        [Fact]
        public async Task AddToCart_Cart_NotNull()
        {

            // arrange
            var db = GetInMemoryDbContext();

            // create the service
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new CartService(db, mapper);

            // act
            var result = await service.AddOrUpdate(new AddToCartDto
            {
                ProductId = 2,
            });

            // assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Collection(result.Items, item =>
            {
                Assert.True(item.Id > 0);
                Assert.Equal(2, item.Product.Id);
            });
        }

        [Fact]
        public async Task AddToCart_Cart_Discount3For2()
        {

            // arrange
            var db = GetInMemoryDbContext();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new CartService(db, mapper);

            // act
            var result = await service.AddOrUpdate(new AddToCartDto
            {
                ProductId = 2,
                Qty = 3
            });

            // assert
            Assert.NotNull(result);
            Assert.Equal(200, result.TotalAfterDiscount);
            Assert.Equal(300, result.Total);
        }

        [Fact]
        public async Task ApplyDiscountCode_Cart_20PercentOff()
        {

            // arrange
            var db = GetInMemoryDbContext();
            
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var service = new CartService(db, mapper);
            await service.AddOrUpdate(new AddToCartDto
            {
                ProductId = 1,
                Qty = 2
            });

            // act
            var result = await service.ApplyDiscountCode(new AddDiscountCodeDto
            {
                CartId = 1,
                DiscountCode = "20OFF"
            });

            // assert
            Assert.NotNull(result);
            Assert.Equal(160, result.TotalAfterDiscount);
            Assert.Equal(200, result.Total);
        }
    }
}
